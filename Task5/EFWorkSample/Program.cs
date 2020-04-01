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
                .Include(od => od.Order.Customer);
            Print(ordersByCategory);
            Console.ReadKey();
        }

        private static void Print(IEnumerable<OrderDetail> ods)
        {
            var newOds = ods.Take(5);
            foreach (var od in newOds)
            {
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", od.Discount, od.Quantity,
                    od.UnitPrice, od.Order.Customer.CompanyName, od.Product.ProductName);
            }

            Console.WriteLine();
        }
    }
}