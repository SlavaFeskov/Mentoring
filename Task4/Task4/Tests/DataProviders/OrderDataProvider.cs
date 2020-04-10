using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindDal.Models.Order;

// ReSharper disable StringLiteralTypo

namespace Tests.DataProviders
{
    public static class OrderDataProvider
    {
        public static IEnumerable<KeyValuePair<OrderState, OrderModel>> GetOrders()
        {
            return new List<KeyValuePair<OrderState, OrderModel>>
            {
                new KeyValuePair<OrderState, OrderModel>(OrderState.InProgress,
                    new OrderModel(DateTime.Parse("7/4/1996 12:00:00 AM"), null)
                    {
                        OrderID = 10248,
                        CustomerID = "ALFKI",
                        EmployeeID = 5,
                        RequiredDate = DateTime.Parse("8/1/1996 12:00:00 AM"),
                        ShipVia = 3,
                        Freight = 32.3800M,
                        ShipName = "Vins et alcools Chevalier",
                        ShipAddress = "59 rue de l'Abbaye",
                        ShipCity = "Reims",
                        ShipPostalCode = "51100",
                        ShipCountry = "France"
                    }),
                new KeyValuePair<OrderState, OrderModel>(OrderState.New,
                    new OrderModel(null, DateTime.Parse("7/10/1996 12:00:00 AM"))
                    {
                        OrderID = 10249,
                        CustomerID = "ANATR",
                        EmployeeID = 1,
                        RequiredDate = DateTime.Parse("8/16/1996 12:00:00 AM"),
                        ShipVia = 1,
                        Freight = 11.6100M,
                        ShipName = "Toms Spezialitäten",
                        ShipAddress = "Luisenstr. 48",
                        ShipCity = "Münster",
                        ShipPostalCode = "44087",
                        ShipCountry = "Germany"
                    }),
                new KeyValuePair<OrderState, OrderModel>(OrderState.Completed,
                    new OrderModel(DateTime.Parse("7/8/1996 12:00:00 AM"), DateTime.Parse("7/12/1996 12:00:00 AM"))
                    {
                        OrderID = 10250,
                        CustomerID = "ANTON",
                        EmployeeID = 4,
                        RequiredDate = DateTime.Parse("8/5/1996 12:00:00 AM"),
                        ShipVia = 2,
                        Freight = 65.8300M,
                        ShipName = "Hanari Carnes",
                        ShipAddress = "Rua do Paço, 67",
                        ShipRegion = "RJ",
                        ShipCity = "Rio de Janeiro",
                        ShipPostalCode = "05454-876",
                        ShipCountry = "Brazil"
                    }),
                new KeyValuePair<OrderState, OrderModel>(OrderState.New,
                    new OrderModel(null, DateTime.Parse("7/15/1996 12:00:00 AM"))
                    {
                        OrderID = 10251,
                        CustomerID = "AROUT",
                        EmployeeID = 3,
                        RequiredDate = DateTime.Parse("8/5/1996 12:00:00 AM"),
                        ShipVia = 1,
                        Freight = 41.3400M,
                        ShipName = "Victuailles en stock",
                        ShipAddress = "2, rue du Commerce",
                        ShipCity = "Lyon",
                        ShipPostalCode = "69004",
                        ShipCountry = "France"
                    }),
                new KeyValuePair<OrderState, OrderModel>(OrderState.InProgress,
                    new OrderModel(DateTime.Parse("7/9/1996 12:00:00 AM"), null)
                    {
                        OrderID = 10252,
                        CustomerID = "BERGS",
                        EmployeeID = 4,
                        RequiredDate = DateTime.Parse("8/6/1996 12:00:00 AM"),
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

        public static KeyValuePair<OrderState, OrderModel> GetRandomOrder()
        {
            var random = new Random();
            var orders = GetOrders().ToList();
            return orders.ElementAt(random.Next(0, orders.Count - 1));
        }
    }
}