using System.Collections.Generic;
using System.Data;

namespace NorthwindDal.Readers.Abstractions
{
    public interface IReader<out TModel>
    {
        TModel ReadSingle(IDataReader reader);

        IEnumerable<TModel> ReadMultiple(IDataReader reader);
    }
}