using System.Collections.Generic;
using System.Data.Common;

namespace NorthwindDal.Readers.Abstractions
{
    public abstract class BaseReader<TModel> : IReader<TModel>
    {
        public virtual TModel ReadSingle(DbDataReader reader)
        {
            return ReadSingleWithOffset(reader, 0);
        }

        public abstract TModel ReadSingleWithOffset(DbDataReader reader, int offset);

        public virtual IEnumerable<TModel> ReadMultiple(DbDataReader reader)
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