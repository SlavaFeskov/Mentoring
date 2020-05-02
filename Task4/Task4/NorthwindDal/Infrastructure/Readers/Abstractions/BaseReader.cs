using System.Collections.Generic;
using System.Data;

namespace NorthwindDal.Infrastructure.Readers.Abstractions
{
    public abstract class BaseReader<TModel> : IReader<TModel>
    {
        public abstract TModel ReadSingle(IDataReader reader);

        public virtual IEnumerable<TModel> ReadMultiple(IDataReader reader)
        {
            var result = new List<TModel>();
            while (reader.Read())
            {
                result.Add(ReadSingle(reader));
            }

            return result;
        }
    }
}