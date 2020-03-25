using System.Data.Common;

namespace NorthwindDal.Services.Abstractions
{
    public interface IConnectionService
    {
        DbConnection CreateAndOpenConnection();
    }
}