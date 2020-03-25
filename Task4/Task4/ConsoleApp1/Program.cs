using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDal.Models;
using NorthwindDal.Models.Order;
using NorthwindDal.Readers.Order;
using NorthwindDal.Readers.OrderDetail;
using NorthwindDal.Repositories.Order;
using NorthwindDal.Repositories.OrderDetail;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = DbProviderFactories.GetFactoryClasses();
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}",
                    row["Name"], row["Description"], row["InvariantName"], row["AssemblyQualifiedName"]);
            }

            var orderReader = new OrderReader();
            var dal = new OrderRepository(
                "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                "System.Data.SqlClient", orderReader);
            var orders = dal.GetOrders().ToList();
            var newOrders = orders.Where(o => o.OrderState == OrderState.New).ToList();
            var inProgress = orders.Where(o => o.OrderState == OrderState.InProgress).ToList();
            var completed = orders.Where(o => o.OrderState == OrderState.Completed).ToList();

            var orderId = completed.First().OrderID;
            var dal1 = new OrderDetailRepository("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                "System.Data.SqlClient", new OrderDetailReader(orderReader));
            var orderDetail = dal1.GetOrderDetailsByOrderId(orderId);
            
            dal.Update(10328, new Dictionary<string, object> { { "Freight", 12 } });
            
            Console.ReadKey();
        }
    }
}