using System;
using System.Data;
using System.Data.Common;
using NorthwindDal.Factories;
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
            var connectionService = new ConnectionFactory(provider,
                "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var readerService = new ReaderService();
            var commandBuilder = new SqlCommandBuilder();
            var orderRepo = new OrderRepository(readerService, connectionService, commandBuilder);
            var orderDetailRepo = new OrderDetailRepository(readerService, connectionService);
            var orderDetails = orderDetailRepo.GetByOrderId(10258);
            var order = orderRepo.GetById(10258);

            Console.ReadKey();
        }
    }
}