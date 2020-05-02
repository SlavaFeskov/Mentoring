using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions.ReportGeneration;

namespace NorthwindHandler.Services.ReportGeneration
{
    public class ReportGeneratorResolver : IReportGeneratorResolver
    {
        private readonly IEnumerable<IReportGenerator> _reportGenerators;
        private const ReportFormat DefaultFormat = ReportFormat.Excel;

        public ReportGeneratorResolver(IEnumerable<IReportGenerator> reportGenerators)
        {
            _reportGenerators = reportGenerators;
        }

        public IReportGenerator Resolve(HttpContext context)
        {
            var acceptHeaderValue = context.Request.Headers["Accept"];
            return _reportGenerators.FirstOrDefault(rg => rg.ContentTypes.Contains(acceptHeaderValue)) ??
                   _reportGenerators.First(rg => rg.Format == DefaultFormat);
        }
    }
}