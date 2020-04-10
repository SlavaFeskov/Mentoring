using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorthwindDal.Extensions;
using NorthwindDal.Factories.Abstractions;
using NorthwindDal.Models.Order;
using NorthwindDal.Readers.Abstractions;
using NorthwindDal.Repositories.Order;
using NorthwindDal.Services.Abstractions;
using Tests.DataProviders;

namespace Tests
{
    [TestClass]
    public class NorthwindDalTests
    {
        private const string OrdersTableName = "Northwind.Orders";
        private Mock<IConnectionFactory> _connectionServiceMock;
        private Mock<ICommandBuilder> _commandBuilderMock;
        private Mock<IReaderService> _readerServiceMock;
        private Mock<IDataReader> _dataReaderMock;
        private Mock<IReader<OrderModel>> _orderReaderMock;

        private void SetUpGetOrderById(int orderId)
        {
            var commandMock = new Mock<IDbCommand>();
            _commandBuilderMock.Setup(m =>
                    m.BuildGetSingleByIdCommand(_connectionServiceMock.Object.CreateAndOpenConnection(),
                        OrdersTableName,
                        OrderData.GetPropertyNames(), new KeyValuePair<string, object>("OrderID", orderId)))
                .Returns(commandMock.Object);
            commandMock.Setup(m => m.ExecuteReader()).Returns(_dataReaderMock.Object);
            _orderReaderMock.Setup(m => m.ReadSingle(_dataReaderMock.Object))
                .Returns(OrderDataProvider.GetOrders().Single(o => o.Value.OrderID == orderId).Value);
        }

        [TestInitialize]
        public void SetUp()
        {
            _connectionServiceMock = new Mock<IConnectionFactory>();
            _connectionServiceMock.Setup(m => m.CreateAndOpenConnection()).Returns(new Mock<IDbConnection>().Object);

            _commandBuilderMock = new Mock<ICommandBuilder>();

            _readerServiceMock = new Mock<IReaderService>();
            _dataReaderMock = new Mock<IDataReader>();
            _orderReaderMock = new Mock<IReader<OrderModel>>();
            _readerServiceMock.SetupGet(m => m.OrderReader).Returns(_orderReaderMock.Object);
        }

        [TestMethod]
        public void GetOrdersTest()
        {
            var commandMock = new Mock<IDbCommand>();
            _commandBuilderMock.Setup(m => m.BuildGetManyCommand(
                _connectionServiceMock.Object.CreateAndOpenConnection(),
                OrdersTableName, OrderData.GetPropertyNames())).Returns(commandMock.Object);

            _orderReaderMock.Setup(m => m.ReadMultiple(_dataReaderMock.Object))
                .Returns(new List<OrderModel> {new OrderModel()});
            commandMock.Setup(m => m.ExecuteReader()).Returns(_dataReaderMock.Object);

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            var actualOrders = orderRepository.GetAll();
            CustomJsonAssert.AreEqual(new List<OrderModel> {new OrderModel()}, actualOrders, "Not equal.");
        }

        [TestMethod]
        public void AddTest()
        {
            var orderToAdd = OrderDataProvider.GetRandomOrder().Value;
            var data = orderToAdd.ToDictionary()
                .Where(kv => kv.Value != null && kv.Key != "OrderID" && kv.Key != "OrderState")
                .ToDictionary(k => k.Key, v => v.Value);
            var commandMock = new Mock<IDbCommand>();
            _commandBuilderMock.Setup(m =>
                    m.BuildAddCommand(_connectionServiceMock.Object.CreateAndOpenConnection(), OrdersTableName, data))
                .Returns(commandMock.Object);
            commandMock.Setup(m => m.ExecuteScalar()).Returns((decimal) orderToAdd.OrderID);

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            var actualOrder = orderRepository.Add(orderToAdd);
            CustomJsonAssert.AreEqual(orderToAdd, actualOrder, "Not equal.");
        }

        [TestMethod]
        public void UpdateSmokeTest()
        {
            var newOrder = OrderDataProvider.GetOrders().First(o => o.Key == OrderState.New);
            SetUpGetOrderById(newOrder.Value.OrderID);
            var orderId = new KeyValuePair<string, object>("OrderID", newOrder.Value.OrderID);
            var data = OrderDataProvider.GetOrderUpdateValues();
            var commandMock = new Mock<IDbCommand>();
            _commandBuilderMock.Setup(m =>
                m.BuildUpdateCommand(_connectionServiceMock.Object.CreateAndOpenConnection(), OrdersTableName, data,
                    orderId)).Returns(commandMock.Object);

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            //var affectedRows = orderRepository.Update(newOrder.Value.OrderID, data);
            //Assert.AreEqual(1, affectedRows, "Update failed.");
        }

        [TestMethod]
        [DataRow(OrderState.InProgress)]
        [DataRow(OrderState.Completed)]
        public void UpdateOrderInProgressOrCompleted(OrderState state)
        {
            var inProgressOrder = OrderDataProvider.GetOrders().First(o => o.Key == state);
            SetUpGetOrderById(inProgressOrder.Value.OrderID);
            var orderId = new KeyValuePair<string, object>("OrderID", inProgressOrder.Value.OrderID);
            var data = OrderDataProvider.GetOrderUpdateValues();
            var commandMock = new Mock<IDbCommand>();
            _commandBuilderMock.Setup(m =>
                m.BuildUpdateCommand(_connectionServiceMock.Object.CreateAndOpenConnection(), OrdersTableName, data,
                    orderId)).Returns(commandMock.Object);
            commandMock.Setup(m => m.ExecuteNonQuery()).Returns(1);

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            //Assert.ThrowsException<InvalidOperationException>(
            //    () => orderRepository.Update(inProgressOrder.Value.OrderID, data),
            //    "Update was completed for order InProgress.");
        }

        [TestMethod]
        [DataRow("OrderDate")]
        [DataRow("ShippedDate")]
        public void UpdateOrderDateOrShippedDateExplicit(string columnToUpdate)
        {
            var newOrder = OrderDataProvider.GetOrders().First(o => o.Key == OrderState.New);
            SetUpGetOrderById(newOrder.Value.OrderID);
            var data = OrderDataProvider.GetOrderUpdateValues();
            data.Add(columnToUpdate, DateTime.Parse("7/7/1997 12:02:00 AM"));

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            //Assert.ThrowsException<InvalidOperationException>(
            //    () => orderRepository.Update(newOrder.Value.OrderID, data),
            //    "Update was completed for order InProgress.");
        }

        [TestMethod]
        public void GetOrderByIdTest()
        {
            var order = OrderDataProvider.GetRandomOrder();
            SetUpGetOrderById(order.Value.OrderID);
            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            var actualOrder = orderRepository.GetById(order.Value.OrderID);
            CustomJsonAssert.AreEqual(order.Value, actualOrder, "Order differs from expected.");
        }

        [TestMethod]
        public void DeleteSmokeTest()
        {
            var order = OrderDataProvider.GetRandomOrder();
            SetUpGetOrderById(order.Value.OrderID);
            var commandMock = new Mock<IDbCommand>();
            _commandBuilderMock.Setup(m => m.BuildDeleteCommand(_connectionServiceMock.Object.CreateAndOpenConnection(),
                    OrdersTableName, new KeyValuePair<string, object>("OrderID", order.Value.OrderID)))
                .Returns(commandMock.Object);
            commandMock.Setup(m => m.ExecuteNonQuery()).Returns(1);

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            //var affectedRows = orderRepository.Delete(order.Value.OrderID);
            //Assert.AreEqual(1, affectedRows, "Delete operation failed.");
        }

        [TestMethod]
        public void DeleteOrderInCompletedState()
        {
            var order = OrderDataProvider.GetOrders().First(o => o.Key == OrderState.Completed);
            SetUpGetOrderById(order.Value.OrderID);

            var orderRepository = new OrderRepository(_readerServiceMock.Object, _connectionServiceMock.Object,
                _commandBuilderMock.Object);
            Assert.ThrowsException<InvalidOperationException>(() => orderRepository.Delete(order.Value.OrderID),
                "Order in Completed state was deleted.");
        }
    }
}