using System.Collections.Generic;
using System.Data;

namespace NorthwindDal.Infrastructure.Readers.Abstractions
{
    public interface IReader<out TModel>
    {
        TModel ReadSingle(IDataReader reader);

        IEnumerable<TModel> ReadMultiple(IDataReader reader);
    }
}