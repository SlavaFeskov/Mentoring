using System.Data;

namespace NorthwindDal.Extensions
{
    public static class DbDataReaderExtension
    {
        public static T GetValueOrDefault<T>(this IDataReader reader, int order) =>
            reader.IsDBNull(order) ? default(T) : (T) reader.GetValue(order);
    }
}