using System.Data;

namespace NorthwindDal.Factories.Abstractions
{
    public interface IConnectionFactory
    {
        IDbConnection CreateAndOpenConnection();
    }
}