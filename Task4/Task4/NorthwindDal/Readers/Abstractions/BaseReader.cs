﻿using System.Collections.Generic;
using System.Data;

namespace NorthwindDal.Readers.Abstractions
{
    public abstract class BaseReader<TModel> : IReader<TModel>
    {
        public virtual TModel ReadSingle(IDataReader reader)
        {
            return ReadSingleWithOffset(reader, 0);
        }

        public abstract TModel ReadSingleWithOffset(IDataReader reader, int offset);

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