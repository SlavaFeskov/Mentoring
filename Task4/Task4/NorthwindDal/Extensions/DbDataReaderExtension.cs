using System.Data.Common;

namespace NorthwindDal.Extensions
{
    public static class DbDataReaderExtension
    {
        public static T GetValueOrDefault<T>(this DbDataReader reader, int order) =>
            reader.IsDBNull(order) ? default(T) : (T) reader.GetValue(order);
    }
}