using System;
using System.Data;
using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Infrastructure.Extensions;
using NorthwindDal.Infrastructure.Readers.Abstractions;

namespace NorthwindDal.Infrastructure.Readers.Order
{
    public class OrderReader : BaseReader<OrderModel>
    {
        public override OrderModel ReadSingle(IDataReader reader)
        {
            var orderDate = reader.GetValueOrDefault<DateTime?>(nameof(OrderModel.OrderDate));
            var shippedDate = reader.GetValueOrDefault<DateTime?>(nameof(OrderModel.ShippedDate));
            var order = new OrderModel(orderDate, shippedDate);
            order.OrderID = reader.GetValueOrDefault<int>(nameof(OrderModel.OrderID));
            order.RequiredDate = reader.GetValueOrDefault<DateTime?>(nameof(OrderModel.RequiredDate));
            order.Freight = reader.GetValueOrDefault<decimal>(nameof(OrderModel.Freight));
            order.ShipName = reader.GetValueOrDefault<string>(nameof(OrderModel.ShipName));
            order.ShipAddress = reader.GetValueOrDefault<string>(nameof(OrderModel.ShipAddress));
            order.ShipCity = reader.GetValueOrDefault<string>(nameof(OrderModel.ShipCity));
            order.ShipRegion = reader.GetValueOrDefault<string>(nameof(OrderModel.ShipRegion));
            order.ShipPostalCode = reader.GetValueOrDefault<string>(nameof(OrderModel.ShipPostalCode));
            order.ShipCountry = reader.GetValueOrDefault<string>(nameof(OrderModel.ShipCountry));
            order.CustomerID = reader.GetValueOrDefault<string>(nameof(OrderModel.CustomerID));
            order.EmployeeID = reader.GetValueOrDefault<int?>(nameof(OrderModel.EmployeeID));
            order.ShipVia = reader.GetValueOrDefault<int?>(nameof(OrderModel.ShipVia));
            return order;
        }
    }
}