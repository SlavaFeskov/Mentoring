using System.Data.Common;
using NorthwindDal.Exceptions;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public ConnectionService(DbProviderFactory providerFactory, string connectionString)
        {
            _providerFactory = providerFactory;
            _connectionString = connectionString;
        }

        public DbConnection CreateAndOpenConnection()
        {
            var connection = _providerFactory.CreateConnection();
            if (connection == null)
            {
                throw new ConnectionException();
            }

            connection.ConnectionString = _connectionString;
            connection.Open();

            return connection;
        }
    }
}