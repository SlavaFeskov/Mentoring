using System.Collections.Generic;
using System.Web;
using NorthwindHandler.Services;
using NorthwindHandler.Services.Abstractions;
using NorthwindHandler.Services.Abstractions.ReportGeneration;
using NorthwindHandler.Services.ReportGeneration;

namespace NorthwindHandler
{
    public class NorthwindReportHandler : IHttpHandler
    {
        private readonly IParamsRetriever _paramsRetriever;
        private readonly IOrdersProvider _ordersProvider;
        private readonly IResponseGenerator _responseGenerator;
        private readonly IReportGeneratorResolver _reportGeneratorResolver;

        public NorthwindReportHandler()
        {
            _paramsRetriever = new ParamsRetriever();
            _ordersProvider = new OrdersProvider();
            _reportGeneratorResolver = new ReportGeneratorResolver(new List<IReportGenerator>
                {new XlsxReportGenerator(), new XmlReportGenerator()});
            _responseGenerator = new ResponseGenerator();
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var filter = _paramsRetriever.GetParameters(context.Request);
            var orders = _ordersProvider.GetOrders(filter);
            var reportGenerator = _reportGeneratorResolver.Resolve(context);
            _responseGenerator.GenerateResponse(reportGenerator, context.Response, orders);
        }
    }
}