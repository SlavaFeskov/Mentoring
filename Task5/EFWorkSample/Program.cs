using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Task5_EF.DbContext;
using Task5_EF.Models;

namespace EFWorkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new NorthwindDbContext();
            var ordersByCategory = context.OrderDetails.Where(od => od.Product.Category.CategoryName == "Seafood")
                .Include(od => od.Order.Customer)
                .Select(e => new
                    {e.OrderID, e.UnitPrice, e.Quantity, e.Product.ProductName, e.Order.Customer.CompanyName });
            var newOds = ordersByCategory.Take(5);
            foreach (var od in newOds)
            {
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", od.OrderID, od.UnitPrice,
                    od.Quantity, od.ProductName, od.CompanyName);
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}