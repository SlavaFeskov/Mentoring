using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions;

namespace NorthwindHandler.Services
{
    public class ResponseGenerator : IResponseGenerator
    {
        private readonly IEnumerable<IReportGenerator> _reportGenerators;
        private const ReportFormat DefaultFormat = ReportFormat.Excel;

        public ResponseGenerator(IEnumerable<IReportGenerator> reportGenerators)
        {
            _reportGenerators = reportGenerators;
        }

        public void GenerateResponse(HttpContext context, IEnumerable<OrderModel> orders)
        {
            var acceptHeaderValue = context.Request.Headers["Accept"];
            var generator = _reportGenerators.FirstOrDefault(rg => rg.ContentTypes.Contains(acceptHeaderValue)) ??
                            _reportGenerators.First(rg => rg.Format == DefaultFormat);
            using var reportStream = generator.Generate(orders);
            reportStream.CopyTo(context.Response.OutputStream);
            context.Response.ContentType = generator.ContentTypes.First();
        }
    }
}