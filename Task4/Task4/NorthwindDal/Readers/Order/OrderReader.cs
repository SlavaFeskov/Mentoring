using System;
using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Order;
using NorthwindDal.Readers.Abstractions;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Readers.Order
{
    public class OrderReader : BaseReader<OrderModel>
    {
        public override OrderModel ReadSingleWithOffset(IDataReader reader, int offset)
        {
            var orderDate = reader.GetValueOrDefault<DateTime?>(1 + offset);
            var shippedDate = reader.GetValueOrDefault<DateTime?>(3 + offset);
            var order = new OrderModel(orderDate, shippedDate);
            order.OrderID = reader.GetValueOrDefault<int>(0 + offset);
            order.RequiredDate = reader.GetValueOrDefault<DateTime?>(2 + offset);
            order.Freight = reader.GetValueOrDefault<decimal>(4 + offset);
            order.ShipName = reader.GetValueOrDefault<string>(5 + offset);
            order.ShipAddress = reader.GetValueOrDefault<string>(6 + offset);
            order.ShipCity = reader.GetValueOrDefault<string>(7 + offset);
            order.ShipRegion = reader.GetValueOrDefault<string>(8 + offset);
            order.ShipPostalCode = reader.GetValueOrDefault<string>(9 + offset);
            order.ShipCountry = reader.GetValueOrDefault<string>(10 + offset);
            order.CustomerID = reader.GetValueOrDefault<string>(11 + offset);
            order.EmployeeID = reader.GetValueOrDefault<int?>(12 + offset);
            order.ShipVia = reader.GetValueOrDefault<int?>(13 + offset);
            return order;
        }
    }
}