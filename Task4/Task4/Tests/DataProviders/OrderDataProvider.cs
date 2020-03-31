using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindDal.Models.Order;

// ReSharper disable StringLiteralTypo

namespace Tests.DataProviders
{
    public static class OrderDataProvider
    {
        public static IEnumerable<KeyValuePair<OrderState, Order>> GetOrders()
        {
            return new List<KeyValuePair<OrderState, Order>>
            {
                new KeyValuePair<OrderState, Order>(OrderState.InProgress, new Order
                {
                    OrderID = 10248,
                    CustomerID = "ALFKI",
                    EmployeeID = 5,
                    OrderDate = DateTime.Parse("7/4/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("8/1/1996 12:00:00 AM"),
                    ShippedDate = null,
                    ShipVia = 3,
                    Freight = 32.3800M,
                    ShipName = "Vins et alcools Chevalier",
                    ShipAddress = "59 rue de l'Abbaye",
                    ShipCity = "Reims",
                    ShipPostalCode = "51100",
                    ShipCountry = "France"
                }),
                new KeyValuePair<OrderState, Order>(OrderState.New, new Order
                {
                    OrderID = 10249,
                    CustomerID = "ANATR",
                    EmployeeID = 1,
                    OrderDate = null,
                    RequiredDate = DateTime.Parse("8/16/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("7/10/1996 12:00:00 AM"),
                    ShipVia = 1,
                    Freight = 11.6100M,
                    ShipName = "Toms Spezialitäten",
                    ShipAddress = "Luisenstr. 48",
                    ShipCity = "Münster",
                    ShipPostalCode = "44087",
                    ShipCountry = "Germany"
                }),
                new KeyValuePair<OrderState, Order>(OrderState.Completed, new Order
                {
                    OrderID = 10250,
                    CustomerID = "ANTON",
                    EmployeeID = 4,
                    OrderDate = DateTime.Parse("7/8/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("8/5/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("7/12/1996 12:00:00 AM"),
                    ShipVia = 2,
                    Freight = 65.8300M,
                    ShipName = "Hanari Carnes",
                    ShipAddress = "Rua do Paço, 67",
                    ShipRegion = "RJ",
                    ShipCity = "Rio de Janeiro",
                    ShipPostalCode = "05454-876",
                    ShipCountry = "Brazil"
                }),
                new KeyValuePair<OrderState, Order>(OrderState.New, new Order
                {
                    OrderID = 10251,
                    CustomerID = "AROUT",
                    EmployeeID = 3,
                    OrderDate = null,
                    RequiredDate = DateTime.Parse("8/5/1996 12:00:00 AM"),
                    ShippedDate = DateTime.Parse("7/15/1996 12:00:00 AM"),
                    ShipVia = 1,
                    Freight = 41.3400M,
                    ShipName = "Victuailles en stock",
                    ShipAddress = "2, rue du Commerce",
                    ShipCity = "Lyon",
                    ShipPostalCode = "69004",
                    ShipCountry = "France"
                }),
                new KeyValuePair<OrderState, Order>(OrderState.InProgress, new Order
                {
                    OrderID = 10252,
                    CustomerID = "BERGS",
                    EmployeeID = 4,
                    OrderDate = DateTime.Parse("7/9/1996 12:00:00 AM"),
                    RequiredDate = DateTime.Parse("8/6/1996 12:00:00 AM"),
                    ShippedDate = null,
                    ShipVia = 2,
                    Freight = 51.3000M,
                    ShipName = "Suprêmes délices",
                    ShipAddress = "Boulevard Tirou, 255",
                    ShipCity = "Charleroi",
                    ShipPostalCode = "B-6000",
                    ShipCountry = "Belgium"
                })
            };
        }

        public static Dictionary<string, object> GetOrderUpdateValues() =>
            new Dictionary<string, object>
            {
                {"Freight", 45.05100},
                {"ShipCity", "Minsk"},
            };

        public static KeyValuePair<OrderState, Order> GetRandomOrder()
        {
            var random = new Random();
            var orders = GetOrders().ToList();
            return orders.ElementAt(random.Next(0, orders.Count - 1));
        }
    }
}