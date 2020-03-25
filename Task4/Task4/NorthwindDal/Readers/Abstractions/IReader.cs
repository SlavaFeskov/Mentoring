using System.Collections.Generic;
using System.Data.Common;

namespace NorthwindDal.Readers.Abstractions
{
    public interface IReader<out TModel>
    {
        TModel ReadSingle(DbDataReader reader);

        TModel ReadSingleWithOffset(DbDataReader reader, int offset);

        IEnumerable<TModel> ReadMultiple(DbDataReader reader);
    }
}