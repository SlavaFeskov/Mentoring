using System.Data.Common;
using NorthwindDal.Repositories.Order;
using NorthwindDal.Services;
using NorthwindDal.Services.Abstractions;
using NUnit.Framework;

namespace NorthwindDalTests
{
    [TestFixture]
    public class NorthwindDalTests
    {
        private const string ProviderName = "System.Data.SqlClient";
        private const string ConnectionString =
            "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;" +
            "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private IReaderService _readerService;
        private IConnectionService _connectionService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _readerService = new ReaderService();
            var dbProvider = DbProviderFactories.GetFactory(ProviderName);
            _connectionService = new ConnectionService(dbProvider, ConnectionString);
        }

        [Test]
        public void GetOrdersTest()
        {
        }
    }
}