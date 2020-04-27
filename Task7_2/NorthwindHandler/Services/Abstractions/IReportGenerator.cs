using System.Collections.Generic;
using System.IO;
using NorthwindHandler.Models;

namespace NorthwindHandler.Services.Abstractions
{
    public interface IReportGenerator
    {
        IEnumerable<string> ContentTypes { get; }
        ReportFormat Format { get; }
        Stream Generate(IEnumerable<OrderModel> orders);
    }
}