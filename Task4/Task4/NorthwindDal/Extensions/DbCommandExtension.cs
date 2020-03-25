using System.Data.Common;

namespace NorthwindDal.Extensions
{
    public static class DbCommandExtension
    {
        public static DbCommand AddParameter(this DbCommand command, string paramName, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value;
            command.Parameters.Add(parameter);
            return command;
        }
    }
}