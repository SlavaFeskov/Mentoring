using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions;

namespace NorthwindHandler.Services
{
    public class OrdersProvider : IOrdersProvider
    {
        public IEnumerable<OrderModel> GetOrders(Filter filter)
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    OrderID = 10248,
                    CustomerID = "ALFKI",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10249,
                    CustomerID = "HGUTJ",
                    OrderDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("7/19/1997 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("10/12/1999 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10250,
                    CustomerID = "OLGMD",
                    OrderDate = DateTime.Parse("8/20/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("6/14/1995 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("1/21/1998 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                },
                new OrderModel
                {
                    OrderID = 10251,
                    CustomerID = "QLVZC",
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("5/19/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("8/1/1996 12:00:00 AM")
                }
            };

            var filtered = orders.Where(filter.IsSatisfied).ToList();
            return filtered.Skip(filter.Skip ?? 0).Take(filter.Take ?? filtered.Count);
        }
    }
}