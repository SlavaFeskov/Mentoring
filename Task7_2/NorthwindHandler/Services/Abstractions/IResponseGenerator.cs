using System.Collections.Generic;
using System.Web;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions.ReportGeneration;

namespace NorthwindHandler.Services.Abstractions
{
    public interface IResponseGenerator
    {
        void GenerateResponse(IReportGenerator generator, HttpResponse response, IEnumerable<OrderModel> orders);
    }
}