using System.Data;

namespace NorthwindDal.Infrastructure.Factories.Abstractions
{
    public interface IConnectionFactory
    {
        IDbConnection CreateAndOpenConnection();
    }
}