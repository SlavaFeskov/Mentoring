using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Microsoft.SqlServer.Dac;
using NorthwindDal.Repositories.Order;
using NorthwindDal.Services;
using NorthwindDal.Services.Abstractions;
using NUnit.Framework;

namespace NorthwindDalTests
{
    [TestFixture]
    public class NorthwindDalTests
    {
        private const string DbFileRelativePath = "TestData/Northwind.dacpac";
        private const string ProviderName = "System.Data.SqlClient";
        private const string ConnectionString =
            "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind_Test;Integrated Security=True";

        private IReaderService _readerService;
        private IConnectionService _connectionService;

        private void DeployTestDb()
        {
            var table = DbProviderFactories.GetFactoryClasses();
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}",
                    row["Name"], row["Description"], row["InvariantName"], row["AssemblyQualifiedName"]);
            }
            var dbFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DbFileRelativePath);
            var ds = new DacServices(ConnectionString);
            ds.Extract(dbFile, "Northwind_Test", "AppName", new Version());
            using var dp = DacPackage.Load(dbFile);
            ds.Deploy(dp, "Northwind_Test");
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //_readerService = new ReaderService();
            //var dbProvider = DbProviderFactories.GetFactory(ProviderName);
            //_connectionService = new ConnectionService(dbProvider, ConnectionString);
        }

        [Test]
        public void GetOrdersTest()
        {
            DeployTestDb();
        }
    }
}