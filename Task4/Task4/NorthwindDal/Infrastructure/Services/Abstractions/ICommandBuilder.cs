using System.Collections.Generic;
using System.Data;

namespace NorthwindDal.Infrastructure.Services.Abstractions
{
    public interface ICommandBuilder
    {
        IDbCommand BuildGetSingleByIdCommand(IDbConnection connection, string tableName, IEnumerable<string> columns,
            KeyValuePair<string, object> id);

        IDbCommand BuildGetManyCommand(IDbConnection connection, string tableName, IEnumerable<string> columns);

        IDbCommand BuildAddCommand(IDbConnection connection, string tableName, IDictionary<string, object> data);

        IDbCommand BuildUpdateCommand(IDbConnection connection, string tableName, IDictionary<string, object> data,
            KeyValuePair<string, object> id);

        IDbCommand BuildDeleteCommand(IDbConnection connection, string tableName, KeyValuePair<string, object> id);

        IDbCommand BuildSpCallCommand(IDbConnection connection, string spName, IDictionary<string, object> parameters);
    }
}