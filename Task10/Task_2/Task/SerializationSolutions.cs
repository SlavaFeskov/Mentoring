using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Task.Services;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        private Northwind _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = NorthwindDbContextFactory.Create();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
            var categories = _dbContext.Categories.ToList();

            var c = categories.First();

            tester.SerializeAndDeserialize(categories);
        }

        [TestMethod]
        public void ISerializable()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
            var products = _dbContext.Products.Take(2).ToList();
            var local = _dbContext.Categories.Local.ToList();

            tester.SerializeAndDeserialize(products);
        }


        [TestMethod]
        public void ISerializationSurrogate()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
            var orderDetails = _dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            _dbContext.Configuration.ProxyCreationEnabled = true;
            _dbContext.Configuration.LazyLoadingEnabled = true;

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Order>>(
                    new DataContractSerializer(typeof(IEnumerable<Order>)), true);
            var orders = _dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}