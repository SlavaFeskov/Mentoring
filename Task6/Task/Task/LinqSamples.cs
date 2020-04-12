// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace Task
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private readonly DataSource dataSource = new DataSource();

        [Category("Task6")]
        [Title("Task 6 - 1")]
        [Description(
            "Returns a list of customers, which have their overall orders sum above than some predefined value.")]
        public void Linq1()
        {
            var totalSums = new List<decimal> { 50000, 100000 };
            var customersTotalRevenues = this.dataSource.Customers.Select(
                c => new
                {
                    CustomerName = c.CompanyName,
                    TotalRevenue = c.Orders.Sum(o => o.Total)
                }).ToList();

            foreach (var totalSum in totalSums)
            {
                var customers = customersTotalRevenues.Where(c => c.TotalRevenue > totalSum);
                ObjectDumper.Write(customers);
                ObjectDumper.Write("\r\n");
            }
        }

        [Category("Task6")]
        [Title("Task 6 - 2")]
        [Description("Returns a list of customers and suppliers from the same city and country.")]
        public void Linq2()
        {
            var suppliers1 = this.dataSource.Customers.Join(
                this.dataSource.Suppliers,
                c => new { c.Country, c.City },
                s => new { s.Country, s.City },
                (customer, supplier) => new
                {
                    customer.CompanyName, supplier.SupplierName,
                    Location = $"'{customer.Country} {customer.City}'"
                });

            var suppliers2 = this.dataSource.Customers.SelectMany(
                c => this.dataSource.Suppliers.Where(s => s.Country == c.Country && s.City == c.City)
                    .Select(s => new { c.CompanyName, s.SupplierName, Location = $"'{c.Country} {c.City}" }));
            ObjectDumper.Write(suppliers1);
            ObjectDumper.Write("\r\n");
            ObjectDumper.Write(suppliers2);
        }

        [Category("Task6")]
        [Title("Task 6 - 3")]
        [Description("Returns a list of customers which have had any order total sum above some predefined value.")]
        public void Linq3()
        {
            var x = 10000M;
            var customers = this.dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > x));
            ObjectDumper.Write(customers);
        }

        private class CustomerWithStartDate
        {
            public DateTime? StartDate { get; set; }

            public Customer Customer { get; set; }
        }

        private IEnumerable<CustomerWithStartDate> GetCustomersWithStartDate() =>
            this.dataSource.Customers.Select(
                c =>
                {
                    DateTime? firstOrderDate = null;
                    if (c.Orders.Length != 0)
                    {
                        firstOrderDate = c.Orders.Min(o => o.OrderDate);
                    }

                    return new CustomerWithStartDate
                    {
                        Customer = c,
                        StartDate = firstOrderDate
                    };
                });

        private IEnumerable<object> CustomerWithStartDateToPrintableObject(
            IEnumerable<CustomerWithStartDate> customerWithStartDates) =>
            customerWithStartDates.Select(
                cs => new
                {
                    CustomerSince = cs.StartDate?.ToString("MM/yyyy") ?? "Not a customer yet.",
                    Customer = cs.Customer.CompanyName
                });

        [Category("Task6")]
        [Title("Task 6 - 4")]
        [Description("Returns a list of customers with their first order date.")]
        public void Linq4()
        {
            ObjectDumper.Write(this.CustomerWithStartDateToPrintableObject(this.GetCustomersWithStartDate()));
        }

        [Category("Task6")]
        [Title("Task 6 - 5")]
        [Description(
            "Returns a list of customers with their first order date ordered descending by year, then descending by month and then descending by orders count for that customer.")]
        public void Linq5()
        {
            var customers = this.GetCustomersWithStartDate().Where(c => c.StartDate.HasValue)
                .OrderByDescending(o => o.StartDate.Value.Year).ThenByDescending(o => o.StartDate.Value.Month)
                .ThenByDescending(o => o.Customer.Orders.Length);
            ObjectDumper.Write(this.CustomerWithStartDateToPrintableObject(customers));
        }

        [Category("Task6")]
        [Title("Task 6 - 6")]
        [Description(
            "Returns a list of customers which have not numeric PostalCode or don't have Region or don't have phone operator (no brackets in phone number).")]
        public void Linq6()
        {
            var postalCodeRegex = new Regex(@"\d+");
            var phoneRegex = new Regex(@"\(\d+\).*");
            var customers = this.dataSource.Customers.Where(
                c => (c.PostalCode != null && !postalCodeRegex.IsMatch(c.PostalCode)) ||
                     string.IsNullOrEmpty(c.Region) ||
                     !phoneRegex.IsMatch(c.Phone));
            ObjectDumper.Write(customers);
        }

        [Category("Task6")]
        [Title("Task 6 - 7")]
        [Description(
            "Groups products by categories, then in each group groups by presence in stock and orders by price in the last group.")]
        public void Linq7()
        {
            var products = this.dataSource.Products.GroupBy(p => p.Category).Select(
                    g => g.GroupBy(pg => pg.UnitsInStock != 0).Select(g1 => g1.OrderBy(p => p.UnitPrice)))
                .SelectMany(g => g.ToList()).SelectMany(g => g.ToList()).Select(
                    p => new { p.Category, InStock = p.UnitsInStock != 0, p.UnitPrice });
            ObjectDumper.Write(products);
        }

        private enum PriceGroup
        {
            NoPrice,
            Cheap,
            Medium,
            Expensive
        }

        [Category("Task6")]
        [Title("Task 6 - 8")]
        [Description(
            "Groups products by UnitPrice (Cheap, Medium, Expensive), the borders for each price group are predefined.")]
        public void Linq8()
        {
            var calcPriceGroup = new Func<Product, PriceGroup>(
                p =>
                {
                    if (p.UnitPrice < 15)
                    {
                        return PriceGroup.Cheap;
                    }

                    if (15 <= p.UnitPrice && p.UnitPrice <= 50)
                    {
                        return PriceGroup.Medium;
                    }

                    if (p.UnitPrice > 50)
                    {
                        return PriceGroup.Expensive;
                    }

                    return PriceGroup.NoPrice;
                });
            var products = this.dataSource.Products.GroupBy(p => calcPriceGroup(p)).OrderBy(g => (int)g.Key)
                .SelectMany(
                    g => g.Select(p => new { PriceGroup = g.Key, Price = p.UnitPrice, Product = p.ProductName })
                        .OrderBy(p => p.Price));
            ObjectDumper.Write(products);
        }

        [Category("Task6")]
        [Title("Task 6 - 9")]
        [Description(
            "Returns list of cities with their average orders overall sum and average intensity of orders for each customer in the city.")]
        public void Linq9()
        {
            var result = this.dataSource.Customers.GroupBy(c => c.City).Select(
                g => new
                {
                    City = g.Key,
                    AvgIncome = Math.Round(g.Select(c => c.Orders.Sum(o => o.Total)).Average(), 2),
                    AvgIntensity = Math.Round(g.Select(c => c.Orders.Length).Average(), 2),
                    NumberOfCustomers = g.Count()
                });
            ObjectDumper.Write(result);
        }

        [Category("Task6")]
        [Title("Task 6 - 10")]
        [Description("Returns statistics for each customer activity by month, year, month of year.")]
        public void Linq10()
        {
            var result1 = this.dataSource.Customers.SelectMany(
                c => c.Orders.GroupBy(
                        o => o.OrderDate.Year,
                        (y, o) => new { Customer = c.CompanyName, Year = y, OrdersCount = o.Count() })
                    .OrderBy(o => o.Year));

            var result2 = this.dataSource.Customers.SelectMany(
                c => c.Orders.GroupBy(
                        o => o.OrderDate.ToString("MMM", CultureInfo.InvariantCulture),
                        (m, o) => new { Customer = c.CompanyName, Month = m, OrdersCount = o.Count() })
                    .OrderBy(o => DateTimeFormatInfo.CurrentInfo?.AbbreviatedMonthNames.ToList().IndexOf(o.Month)));

            var result3 = this.dataSource.Customers.SelectMany(
                c => c.Orders.GroupBy(
                        o => new { Month = o.OrderDate.ToString("MMM"), o.OrderDate.Year },
                        (d, o) => new
                            { Customer = c.CompanyName, MonthYear = $"{d.Month}, {d.Year}", OrdersCount = o.Count() })
                    .OrderBy(o => DateTime.Parse(o.MonthYear)));

            ObjectDumper.Write("Stat per year:\r\n");
            ObjectDumper.Write(result1);
            ObjectDumper.Write("\r\nStat per month (not including year):\r\n");
            ObjectDumper.Write(result2);
            ObjectDumper.Write("\r\nStat per month (include year):\r\n");
            ObjectDumper.Write(result3);
        }
    }
}