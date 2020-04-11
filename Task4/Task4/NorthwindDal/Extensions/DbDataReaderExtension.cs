using System;
using System.Data;

namespace NorthwindDal.Extensions
{
    public static class DbDataReaderExtension
    {
        public static T GetValueOrDefault<T>(this IDataReader reader, int order) =>
            reader.IsDBNull(order) ? default(T) : (T) reader.GetValue(order);

        public static T GetValueOrDefault<T>(this IDataReader reader, string columnName) =>
            Convert.IsDBNull(reader[columnName]) ? default(T) : (T) reader[columnName];
    }
}