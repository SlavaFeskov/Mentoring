using System.Collections.Generic;
using System.Data;

namespace NorthwindDal.Readers.Abstractions
{
    public interface IReader<out TModel>
    {
        TModel ReadSingle(IDataReader reader);

        TModel ReadSingleWithOffset(IDataReader reader, int offset);

        IEnumerable<TModel> ReadMultiple(IDataReader reader);
    }
}