using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDal.Models;
using NorthwindDal.Models.Dto;
using NorthwindDal.Models.Order;
using NorthwindDal.Readers.Order;
using NorthwindDal.Readers.OrderDetail;
using NorthwindDal.Repositories.Order;
using NorthwindDal.Repositories.OrderDetail;
using NorthwindDal.Services;

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

            var provider = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connectionService = new ConnectionService(provider,
                "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var readerService = new ReaderService();
            var commandBuilder = new SqlCommandBuilder();
            var orderRepo = new OrderRepository(readerService, connectionService, commandBuilder);
            var orderDetailRepo = new OrderDetailRepository(readerService, connectionService);

            var orderDetails = orderDetailRepo.GetOrderDetailsByOrderId(10258);

            var orders = orderRepo.TakeOrderInProgress(11078, DateTime.Parse("7/9/1996 12:00:00 AM"));

            Console.ReadKey();
        }
    }
}