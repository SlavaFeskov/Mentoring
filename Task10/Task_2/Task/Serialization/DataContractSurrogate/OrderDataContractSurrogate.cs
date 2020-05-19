using System;
using System.Linq;
using Task.DB;

namespace Task.Serialization.DataContractSurrogate
{
    public class OrderDataContractSurrogate : Abstractions.DataContractSurrogate
    {
        private readonly Northwind _dbContext;

        public OrderDataContractSurrogate(Northwind dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override Type GetDataContractType(Type type)
        {
            if (typeof(Order).IsAssignableFrom(type))
            {
                return typeof(Order);
            }

            return type;
        }

        public override object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Order order)
            {
                return new Order
                {
                    OrderID = order.OrderID,
                    CustomerID = order.CustomerID,
                    EmployeeID = order.EmployeeID,
                    OrderDate = order.OrderDate,
                    RequiredDate = order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    ShipVia = order.ShipVia,
                    Freight = order.Freight,
                    ShipName = order.ShipName,
                    ShipAddress = order.ShipAddress,
                    ShipCity = order.ShipCity,
                    ShipRegion = order.ShipRegion,
                    ShipPostalCode = order.ShipPostalCode,
                    ShipCountry = order.ShipCountry,
                    Customer = _dbContext.Customers.Single(c => c.CustomerID == order.CustomerID),
                    Employee = _dbContext.Employees.Single(e => e.EmployeeID == order.EmployeeID),
                    Order_Details = _dbContext.Order_Details.Where(od => od.OrderID == order.OrderID).ToList(),
                    Shipper = _dbContext.Shippers.Single(s => s.ShipperID == order.ShipVia)
                };
            }

            return obj;
        }
    }
}