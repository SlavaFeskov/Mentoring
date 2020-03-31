using System.Collections.Generic;
using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Repositories.OrderDetail
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IReaderService _readers;
        private readonly IConnectionService _connectionService;

        public OrderDetailRepository(IReaderService readers, IConnectionService connectionService)
        {
            _readers = readers;
            _connectionService = connectionService;
        }

        public IEnumerable<Models.OrderDetail.OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            using var connection = _connectionService.CreateAndOpenConnection();

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