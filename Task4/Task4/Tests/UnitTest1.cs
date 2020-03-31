using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.SqlServer.Dac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindDal.Models.Order;
using NorthwindDal.Repositories.Order;
using NorthwindDal.Services;
using Tests.DataProviders;
// ReSharper disable StringLiteralTypo

namespace Tests
{
    [TestClass]
    public class Tests1
    {
        private const string DbFileRelativePath = "Data\\Northwind.dacpac";
        private const string ProviderName = "System.Data.SqlClient";

        private const string ConnectionString =
            "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind_Test;Integrated Security=True";

        private IOrderRepository _orderRepository;

        private void DeployTestDb()
        {
            var dbFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                DbFileRelativePath);
            var ds = new DacServices(ConnectionString);
            ds.Extract(dbFile, "Northwind_Test", "AppName", new Version());
            using var dp = DacPackage.Load(dbFile);
            ds.Deploy(dp, "Northwind_Test", true);
        }

        [TestInitialize]
        public void SetUp()
        {
            DeployTestDb();
            var readerService = new ReaderService();
            var dbProvider = DbProviderFactories.GetFactory(ProviderName);
            var connectionService = new ConnectionService(dbProvider, ConnectionString);
            var commandBuilder = new SqlCommandBuilder();
            _orderRepository = new OrderRepository(readerService, connectionService, commandBuilder);
        }

        [TestMethod]
        public void GetOrdersTest()
        {
            var expectedOrders = OrderDataProvider.GetOrders().ToList();
            var actualOrders = _orderRepository.GetOrders().ToList();
            CustomJsonAssert.AreEqual(expectedOrders, actualOrders,
                "GetOrders method returned incorrect list of orders.");
        }

        [TestMethod]
        public void AddTest()
        {
            var orderToAdd = new Order
            {
                CustomerID = "ALFKI",
                EmployeeID = 5,
                OrderDate = null,
                RequiredDate = null,
                ShippedDate = null,
                ShipVia = 3,
                Freight = 32.3800M,
                ShipName = "Vins et alcools Chevalier",
                ShipAddress = "59 rue de l'Abbaye",
                ShipCity = "Reims",
                ShipPostalCode = "51100",
                ShipCountry = "France"
            };
            var addedOrder = _orderRepository.Add(orderToAdd);
            Assert.AreNotEqual(0, addedOrder.OrderID, "Newly added order as OrderID = 0.");
            var orderInDb = _orderRepository.GetOrderById(addedOrder.OrderID);
            CustomJsonAssert.AreEqual(orderToAdd, orderInDb, "Order in Db is not equal to the added order.");
        }

        [TestMethod]
        public void UpdateTest()
        {
        }
    }
}