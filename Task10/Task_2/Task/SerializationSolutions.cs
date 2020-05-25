using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Task.Serialization;
using Task.Serialization.DataContractSurrogate;
using Task.Serialization.DataContractSurrogate.Abstractions;
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
            _dbContext = NorthwindDbContextProvider.Get();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
            var categories = _dbContext.Categories.Take(2).ToList();

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

            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Order_Detail), new StreamingContext(StreamingContextStates.All),
                new OrderDetailsSerializationSurrogate(_dbContext));
            var serializer = new NetDataContractSerializer {SurrogateSelector = surrogateSelector};

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);
            var orderDetails = _dbContext.Order_Details.Take(2).ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            _dbContext.Configuration.ProxyCreationEnabled = true;
            _dbContext.Configuration.LazyLoadingEnabled = true;

            DataContractSurrogate surrogate = new OrderDataContractSurrogate(_dbContext);
            surrogate = new CustomerDataContractSurrogateDecorator(surrogate);
            surrogate = new EmployeeDataContractSurrogateDecorator(surrogate);
            surrogate = new OrderDetailDataContractSurrogateDecorator(surrogate);
            surrogate = new ShipperDataContractSurrogateDecorator(surrogate);
            var serializer = new DataContractSerializer(typeof(IEnumerable<Order>),
                new[] {typeof(Order), typeof(Customer)}, int.MaxValue, true, false, surrogate);

            var tester =
                new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
            var orders = _dbContext.Orders.Take(2).ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}