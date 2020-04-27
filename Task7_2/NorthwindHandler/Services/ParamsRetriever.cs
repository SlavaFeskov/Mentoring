using System;
using System.Web;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions;

namespace NorthwindHandler.Services
{
    public class ParamsRetriever : IParamsRetriever
    {
        public Filter GetParameters(HttpRequest request)
        {
            var filter = new Filter();
            filter.CustomerId = request["customer"];
            filter.DateFrom = GetDateTimeParameter(request["dateFrom"]);
            filter.DateTo = GetDateTimeParameter(request["dateTo"]);
            filter.Take = GetIntParameter(request["take"]);
            filter.Skip = GetIntParameter(request["skip"]);
            return filter;
        }

        private DateTime? GetDateTimeParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return default;
            }

            if (!DateTime.TryParse(parameter, out var result))
            {
                throw new ArgumentException($"{parameter} wasn't recognized as valid {nameof(DateTime)}.");
            }

            return result;
        }

        private int? GetIntParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return default;
            }

            if (!int.TryParse(parameter, out var result))
            {
                throw new ArgumentException($"{parameter} wasn't recognized as valid {nameof(Int32)}.");
            }

            return result;
        }
    }
}