using System.Collections.Generic;
using System.Linq;
using NorthwindDal.Exceptions;
using NorthwindDal.Extensions;
using NorthwindDal.Factories.Abstractions;
using NorthwindDal.Models.Order;
using NorthwindDal.Services.Abstractions;

// ReSharper disable StringLiteralTypo

namespace NorthwindDal.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private const string OrdersTableName = "Northwind.Orders";
        private readonly IReaderService _readers;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ICommandBuilder _commandBuilder;

        public OrderRepository(
            IReaderService readers,
            IConnectionFactory connectionFactory,
            ICommandBuilder commandBuilder)
        {
            _readers = readers;
            _connectionFactory = connectionFactory;
            _commandBuilder = commandBuilder;
        }

        public IEnumerable<OrderModel> GetAll()
        {
            using var connection = _connectionFactory.CreateAndOpenConnection();
            using var command = _commandBuilder
                .BuildGetManyCommand(connection, OrdersTableName, OrderData.GetPropertyNames());
            using var reader = command.ExecuteReader();
            return _readers.OrderReader.ReadMultiple(reader);
        }

        public OrderModel Add(OrderModel orderModel)
        {
            using var connection = _connectionFactory.CreateAndOpenConnection();
            var orderAsPropDictionary = orderModel.ToDictionary()
                .Where(kv => kv.Value != null && kv.Key != "OrderID")
                .ToDictionary(k => k.Key, v => v.Value);
            using var command = _commandBuilder.BuildAddCommand(connection, OrdersTableName, orderAsPropDictionary);
            var orderId = (decimal) command.ExecuteScalar();
            orderModel.OrderID = (int) orderId;

            return orderModel;
        }

        public void Update(OrderModel order)
        {
            order.ValidateOrderForUpdate();
            var values = order.ToDictionary();
            using var connection = _connectionFactory.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildUpdateCommand(connection, OrdersTableName, values,
                new KeyValuePair<string, object>("OrderID", order.OrderID));
            command.ExecuteNonQuery();
        }

        public OrderModel GetById(int orderId)
        {
            using var connection = _connectionFactory.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildGetSingleByIdCommand(connection, OrdersTableName,
                OrderData.GetPropertyNames(), new KeyValuePair<string, object>("OrderID", orderId));
            using var reader = command.ExecuteReader();
            reader.Read();
            var order = _readers.OrderReader.ReadSingle(reader);
            if (order == null)
            {
                throw new RecordNotFoundException($"Order with {orderId} id was not found.");
            }

            return order;
        }

        public void Delete(int orderId)
        {
            var orderInDb = GetById(orderId);
            orderInDb.ValidateOrderForDeletion();
            using var connection = _connectionFactory.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildDeleteCommand(connection, OrdersTableName,
                new KeyValuePair<string, object>("OrderID", orderId));
            command.ExecuteNonQuery();
        }

        public IEnumerable<CustomerOrderHistory> GetCustomerOrderHistories(int customerId)
        {
            using var connection = _connectionFactory.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildSpCallCommand(connection, "Northwind.CustOrderHist",
                new Dictionary<string, object> {{"CustomerID", customerId}});
            using var reader = command.ExecuteReader();
            return _readers.CustomerOrderHistoryReader.ReadMultiple(reader);
        }

        public IEnumerable<CustomerOrdersDetail> GetCustomerOrdersDetails(int orderId)
        {
            using var connection = _connectionFactory.CreateAndOpenConnection();
            using var command = _commandBuilder.BuildSpCallCommand(connection, "Northwind.CustOrdersDetail",
                new Dictionary<string, object> {{"OrderID", orderId}});
            using var reader = command.ExecuteReader();
            return _readers.CustomerOrderDetailsReader.ReadMultiple(reader);
        }
    }
}