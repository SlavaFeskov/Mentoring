using System.Data;

namespace NorthwindDal.Extensions
{
    public static class DbCommandExtension
    {
        public static IDbCommand AddParameter(this IDbCommand command, string paramName, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value;
            command.Parameters.Add(parameter);
            return command;
        }
    }
}