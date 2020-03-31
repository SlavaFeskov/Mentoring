using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindDal.Data;
using NorthwindDal.Exceptions;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Dto;
using NorthwindDal.Models.Order;
using NorthwindDal.Models.Order.StoredProceduresModels;
using NorthwindDal.Services.Abstractions;

// ReSharper disable StringLiteralTypo

namespace NorthwindDal.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private const string OrdersTableName = "Northwind.Orders";
        private readonly IReaderService _readers;
        private readonly IConnectionService _connectionService;
        private readonly ICommandBuilder _commandBuilder;

        public OrderRepository(
            IReaderService readers,
            IConnectionService connectionService,
            ICommandBuilder commandBuilder)
        {
            _readers = readers;
            _connectionService = connectionService;
            _commandBuilder = commandBuilder;
        }

        public IEnumerable<Models.Order.Order> GetOrders()
        {
            using var connection = _connectionService.CreateAndOpenConnection();
            using var command = _commandBuilder
                .BuildGetManyCommand(connection, OrdersTableName, OrderQueryData.GetQueryColumns);
            using var reader = command.ExecuteReader();
            return _readers.OrderReader.ReadMultiple(reader);
        }

        public Models.Order.Order Add(Models.Order.Order order)
        {
            using var connection = _connectionService.CreateAndOpenConnection();
            var orderAsPropDictionary = order.ToDictionary()
                .Where(kv => kv.Value != null && kv.Key != "OrderID" && kv.Key != "OrderState")
                .ToDictionary(k => k.Key, v => v.Value);
            using var command = _commandBuilder.BuildAddCommand(connection, OrdersTableName, orderAsPropDictionary);
            var orderId = (decimal) command.ExecuteScalar();
            order.OrderID = (int) orderId;

            return order;
        }

        public int Update(OrderDto order) => Update(order.OrderID, order.ToDictionary());

        public int Update(int orderId, IDictionary<string, object> values)
        {
            var orderInDb = GetOrderById(orderId);
            values.Remove("OrderID");
            ThrowExceptionIfOrderInProgressOrCompletedStatusIsUpdated(orderInDb);
            ThrowExceptionIfOrderDateOrShippedDateIsBeingUpdatedExplicitly(values);

            using var connection = _connectionService.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildUpdateCommand(connection, OrdersTableName, values,
                new KeyValuePair<string, object>("OrderID", orderId));
            return command.ExecuteNonQuery();
        }

        public Models.Order.Order GetOrderById(int orderId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildGetSingleByIdCommand(connection, OrdersTableName,
                OrderQueryData.GetQueryColumns, new KeyValuePair<string, object>("OrderID", orderId));
            using var reader = command.ExecuteReader();
            reader.Read();
            var order = _readers.OrderReader.ReadSingle(reader);
            if (order == null)
            {
                throw new RecordNotFoundException($"Order with {orderId} id was not found.");
            }

            return order;
        }

        public int Delete(int orderId)
        {
            var orderInDb = GetOrderById(orderId);
            ThrowExceptionIfOrderInCompletedStateIsBeingDeleted(orderInDb);

            using var connection = _connectionService.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildDeleteCommand(connection, OrdersTableName,
                new KeyValuePair<string, object>("OrderID", orderId));
            return command.ExecuteNonQuery();
        }

        public int TakeOrderInProgress(int orderId, DateTime orderDate)
        {
            var orderInDb = GetOrderById(orderId);
            if (orderInDb.OrderState == OrderState.New)
            {
                using var connection = _connectionService.CreateAndOpenConnection();
                using var command = _commandBuilder.BuildUpdateCommand(connection, OrdersTableName,
                    new Dictionary<string, object> {{"OrderDate", orderDate}},
                    new KeyValuePair<string, object>("OrderID", orderId));
                return command.ExecuteNonQuery();
            }

            return 0;
        }

        public int CompleteOrder(int orderId, DateTime shippedDate)
        {
            var orderInDb = GetOrderById(orderId);
            if (orderInDb.OrderState == OrderState.InProgress)
            {
                using var connection = _connectionService.CreateAndOpenConnection();
                using var command = _commandBuilder.BuildUpdateCommand(connection, OrdersTableName,
                    new Dictionary<string, object> {{"ShippedDate", shippedDate}},
                    new KeyValuePair<string, object>("OrderID", orderId));
                return command.ExecuteNonQuery();
            }

            return 0;
        }

        public IEnumerable<CustomerOrderHistory> GetCustomerOrderHistories(int customerId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildSpCallCommand(connection, "Northwind.CustOrderHist",
                new Dictionary<string, object> {{"CustomerID", customerId}});
            using var reader = command.ExecuteReader();
            return _readers.CustomerOrderHistoryReader.ReadMultiple(reader);
        }

        public IEnumerable<CustomerOrdersDetail> GetCustomerOrdersDetails(int orderId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildSpCallCommand(connection, "Northwind.CustOrdersDetail",
                new Dictionary<string, object> {{"OrderID", orderId}});
            using var reader = command.ExecuteReader();
            return _readers.CustomerOrderDetailsReader.ReadMultiple(reader);
        }

        private void ThrowExceptionIfOrderInProgressOrCompletedStatusIsUpdated(Models.Order.Order order)
        {
            if (order.OrderState == OrderState.InProgress || order.OrderState == OrderState.Completed)
            {
                throw new InvalidOperationException("Cannot update an order with InProgress or Completed status.");
            }
        }

        private void ThrowExceptionIfOrderDateOrShippedDateIsBeingUpdatedExplicitly(IDictionary<string, object> values)
        {
            if (values.ContainsKey("OrderDate") || values.ContainsKey("ShippedDate"))
            {
                throw new InvalidOperationException("Cannot explicitly set OrderDate or ShippedDate for order.");
            }
        }

        private void ThrowExceptionIfOrderInCompletedStateIsBeingDeleted(Models.Order.Order order)
        {
            if (order.OrderState == OrderState.Completed)
            {
                throw new InvalidOperationException("Cannot delete order in Completed state.");
            }
        }
    }
}