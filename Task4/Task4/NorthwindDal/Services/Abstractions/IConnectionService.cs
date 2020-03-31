using System.Data;

namespace NorthwindDal.Services.Abstractions
{
    public interface IConnectionService
    {
        IDbConnection CreateAndOpenConnection();
    }
}