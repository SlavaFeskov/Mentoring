using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions;
using NorthwindHandler.Services.Abstractions.ReportGeneration;

namespace NorthwindHandler.Services
{
    public class ResponseGenerator : IResponseGenerator
    {
        public void GenerateResponse(IReportGenerator generator, HttpResponse response, 
            IEnumerable<OrderModel> orders)
        {
            using var reportStream = generator.Generate(orders);
            reportStream.CopyTo(response.OutputStream);
            response.ContentType = generator.ContentTypes.First();
        }
    }
}