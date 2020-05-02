using System.Data;
using System.Data.Common;
using NorthwindDal.Infrastructure.Exceptions;
using NorthwindDal.Infrastructure.Factories.Abstractions;

namespace NorthwindDal.Infrastructure.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public ConnectionFactory(DbProviderFactory providerFactory, string connectionString)
        {
            _providerFactory = providerFactory;
            _connectionString = connectionString;
        }

        public IDbConnection CreateAndOpenConnection()
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