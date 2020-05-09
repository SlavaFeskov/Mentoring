using System.Web;

namespace NorthwindHandler.Services.Abstractions.ReportGeneration
{
    public interface IReportGeneratorResolver
    {
        IReportGenerator Resolve(HttpContext context);
    }
}