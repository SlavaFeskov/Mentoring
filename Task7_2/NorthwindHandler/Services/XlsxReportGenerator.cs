using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Report;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions;

namespace NorthwindHandler.Services
{
    public class XlsxReportGenerator : IReportGenerator
    {
        private const string TemplateName = "OrdersReportTemplate.xlsx";

        public IEnumerable<string> ContentTypes => new List<string>
            {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"};

        public ReportFormat Format => ReportFormat.Excel;

        public Stream Generate(IEnumerable<OrderModel> orders)
        {
            var memoryStream = new MemoryStream();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportTemplates", TemplateName);
            var template = new XLTemplate(path);
            template.AddVariable("Orders", orders);
            template.Generate();
            template.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}