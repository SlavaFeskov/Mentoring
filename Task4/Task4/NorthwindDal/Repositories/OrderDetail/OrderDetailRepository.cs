using System.Collections.Generic;
using System.Data;
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
                                  "WHERE od.OrderID = @p1";
            command.CommandType = CommandType.Text;
            var orderIdParam = command.CreateParameter();
            orderIdParam.ParameterName = "@p1";
            orderIdParam.Value = orderId;
            command.Parameters.Add(orderIdParam);

            using var reader = command.ExecuteReader();
            return _readers.OrderDetailReader.ReadMultiple(reader);
        }
    }
}