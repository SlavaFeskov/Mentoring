using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NorthwindDal.Extensions;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Services
{
    public class SqlCommandBuilder : ICommandBuilder
    {
        public IDbCommand BuildGetSingleByIdCommand(IDbConnection connection, string tableName, IEnumerable<string> columns,
            KeyValuePair<string, object> id)
        {
            var command = connection.CreateCommand();
            var columnList = string.Join(", ", columns);
            command.CommandText =
                $"SELECT {columnList} " +
                $"FROM {tableName} " +
                $"WHERE {id.Key} = @{id.Key}";
            command.CommandType = CommandType.Text;
            command.AddParameter($"@{id.Key}", id.Value);
            return command;
        }

        public IDbCommand BuildGetManyCommand(IDbConnection connection, string tableName, IEnumerable<string> columns)
        {
            var command = connection.CreateCommand();
            var columnList = string.Join(", ", columns);
            command.CommandText = $"SELECT {columnList} FROM {tableName};";
            command.CommandType = CommandType.Text;
            return command;
        }

        public IDbCommand BuildAddCommand(IDbConnection connection, string tableName, IDictionary<string, object> data)
        {
            var command = connection.CreateCommand();
            foreach (var param in data)
            {
                command.AddParameter($"@{param.Key}", param.Value);
            }

            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(k => $"@{k}"));
            command.CommandText =
                $"INSERT INTO {tableName} ({columns}) " +
                $"VALUES ({values});" +
                "SELECT SCOPE_IDENTITY();";
            command.CommandType = CommandType.Text;
            return command;
        }

        public IDbCommand BuildUpdateCommand(IDbConnection connection, string tableName, IDictionary<string, object> data,
            KeyValuePair<string, object> id)
        {
            var command = connection.CreateCommand();
            var parameters = new List<string>();
            foreach (var value in data)
            {
                parameters.Add($"{value.Key} = @{value.Key}");
                command.AddParameter($"@{value.Key}", value.Value);
            }

            command.AddParameter($"@{id.Key}", id.Value);
            command.CommandText = $"UPDATE Northwind.Orders SET {string.Join(", ", parameters)} " +
                                  $"WHERE {id.Key} = @{id.Key}";
            command.CommandType = CommandType.Text;
            return command;
        }

        public IDbCommand BuildDeleteCommand(IDbConnection connection, string tableName, KeyValuePair<string, object> id)
        {
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {tableName} WHERE {id.Key} = @{id.Key}";
            command.CommandType = CommandType.Text;
            command.AddParameter($"@{id.Key}", id.Value);
            return command;
        }

        public IDbCommand BuildSpCallCommand(IDbConnection connection, string spName, IDictionary<string, object> parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in parameters)
            {
                command.AddParameter($"@{parameter.Key}", parameter.Value);
            }

            return command;
        }
    }
}