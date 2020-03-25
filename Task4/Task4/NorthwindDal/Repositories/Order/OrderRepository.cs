using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NorthwindDal.Exceptions;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Dto;
using NorthwindDal.Models.Order;
using NorthwindDal.Models.Order.StoredProceduresModels;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IReaderService _readers;
        private readonly IConnectionService _connectionService;

        public OrderRepository(IReaderService readers, IConnectionService connectionService)
        {
            _readers = readers;
            _connectionService = connectionService;
        }

        public IEnumerable<Models.Order.Order> GetOrders()
        {
            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT OrderID, OrderDate, RequiredDate, ShippedDate, " +
                                  "Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, " +
                                  "ShipCountry, CustomerID, EmployeeID, ShipVia FROM Northwind.Orders";
            command.CommandType = CommandType.Text;

            using var reader = command.ExecuteReader();
            return _readers.OrderReader.ReadMultiple(reader);
        }

        public Models.Order.Order Add(Models.Order.Order order)
        {
            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            var orderAsPropDictionary = order.ToDictionary();
            orderAsPropDictionary.Remove("OrderID");

            foreach (var param in orderAsPropDictionary)
            {
                command.AddParameter($"@{param.Key}", param.Value);
            }

            var columns = string.Join(", ", orderAsPropDictionary.Keys);
            var values = string.Join(", ", orderAsPropDictionary.Keys.Select(k => $"@{k}"));
            command.CommandText =
                $"INSERT INTO Northwind.Orders ({columns}) " +
                $"VALUES ({values});" +
                "SELECT SCOPE_IDENTITY();";
            command.CommandType = CommandType.Text;

            var orderId = (int) command.ExecuteScalar();
            order.OrderID = orderId;

            return order;
        }

        public int Update(OrderDto order) => Update(order.OrderID, order.ToDictionary());

        public int Update(int orderId, IDictionary<string, object> values)
        {
            var orderInDb = GetOrderById(orderId);
            ThrowExceptionIfOrderInProgressOrCompletedStatusIsUpdated(orderInDb);
            ThrowExceptionIfOrderDateOrShippedDateIsBeingUpdatedExplicitly(values);

            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            var parameters = new List<string>();
            foreach (var value in values)
            {
                parameters.Add($"{value.Key} = @{value.Key}");
                command.AddParameter($"@{value.Key}", value.Value);
            }

            command.AddParameter("@orderId", orderId);
            command.CommandText = $"UPDATE Northwind.Orders SET {string.Join(", ", parameters)} " +
                                  "WHERE OrderID = @orderId";
            command.CommandType = CommandType.Text;
            return command.ExecuteNonQuery();
        }

        public Models.Order.Order GetOrderById(int orderId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            command.CommandText =
                $"SELECT {string.Join(", ", typeof(Models.Order.Order).GetPropertyNames())} " +
                "FROM Northwind.Orders " +
                "WHERE OrderID = @orderId";
            command.CommandType = CommandType.Text;
            command.AddParameter("@orderId", orderId);

            using var reader = command.ExecuteReader();
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
            ThrowExceptionIfOrderInDeletedStateIsBeingDeleted(orderInDb);

            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Northwind.Orders WHERE OrderId = @orderId";
            command.CommandType = CommandType.Text;
            command.AddParameter("@orderId", orderId);

            return command.ExecuteNonQuery();
        }

        public int TakeOrderInProgress(int orderId, DateTime orderDate)
        {
            var orderInDb = GetOrderById(orderId);
            if (orderInDb.OrderState == OrderState.New)
            {
                using var connection = _connectionService.CreateAndOpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText =
                    "UPDATE Northwin.Orders SET OrderDate = @orderDate WHERE OrderID = @orderId";
                command.CommandType = CommandType.Text;
                command.AddParameter("@orderId", orderId);
                command.AddParameter("@orderDate", orderDate);

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

                using var command = connection.CreateCommand();
                command.CommandText =
                    "UPDATE Northwin.Orders SET ShippedDate = @shippedDate WHERE OrderID = @orderId";
                command.CommandType = CommandType.Text;
                command.AddParameter("@orderId", orderId);
                command.AddParameter("@shippedDate", shippedDate);

                return command.ExecuteNonQuery();
            }

            return 0;
        }

        public IEnumerable<CustomerOrderHistory> GetCustomerOrderHistories(int customerId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            command.CommandText = "Northwind.CustOrderHist";
            command.CommandType = CommandType.StoredProcedure;
            command.AddParameter("@CustomerID", customerId);

            using var reader = command.ExecuteReader();
            return _readers.CustomerOrderHistoryReader.ReadMultiple(reader);
        }

        public IEnumerable<CustomerOrdersDetail> GetCustomerOrdersDetails(int orderId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            command.CommandText = "Northwind.CustOrdersDetail";
            command.CommandType = CommandType.StoredProcedure;
            command.AddParameter("@OrderID", orderId);

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

        private void ThrowExceptionIfOrderInDeletedStateIsBeingDeleted(Models.Order.Order order)
        {
            if (order.OrderState == OrderState.Completed)
            {
                throw new InvalidOperationException("Cannot delete order in Completed state.");
            }
        }
    }
}