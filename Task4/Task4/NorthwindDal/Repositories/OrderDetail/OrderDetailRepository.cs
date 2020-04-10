using System.Collections.Generic;
using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Factories.Abstractions;
using NorthwindDal.Models.OrderDetail;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Repositories.OrderDetail
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IReaderService _readers;
        private readonly IConnectionFactory _connectionFactory;

        public OrderDetailRepository(IReaderService readers, IConnectionFactory connectionFactory)
        {
            _readers = readers;
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<OrderDetailModel> GetByOrderId(int orderId)
        {
            using var connection = _connectionFactory.CreateAndOpenConnection();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT od.ProductID, od.UnitPrice, od.Quantity, od.Discount, od.OrderID, " +
                                  "o.OrderDate, o.RequiredDate, o.ShippedDate, o.Freight, o.ShipName, " +
                                  "o.ShipAddress, o.ShipCity, o.ShipRegion, o.ShipPostalCode, o.ShipCountry, " +
                                  "o.CustomerID, o.EmployeeID, o.ShipVia, p.ProductName " +
                                  "FROM Northwind.[Order Details] as od " +
                                  "JOIN Northwind.Orders as o " +
                                  "ON o.OrderID = od.OrderID " +
                                  "JOIN Northwind.Products as p " +
                                  "ON p.ProductID = od.ProductID " +
                                  "WHERE od.OrderID = @orderId";
            command.CommandType = CommandType.Text;
            command.AddParameter("orderId", orderId);

            using var reader = command.ExecuteReader();
            return _readers.OrderDetailReader.ReadMultiple(reader);
        }
    }
}