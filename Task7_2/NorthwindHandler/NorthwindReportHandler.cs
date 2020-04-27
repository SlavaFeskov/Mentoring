using System.Collections.Generic;
using System.Web;
using NorthwindHandler.Services;
using NorthwindHandler.Services.Abstractions;

namespace NorthwindHandler
{
    public class NorthwindReportHandler : IHttpHandler
    {
        private readonly IParamsRetriever _paramsRetriever;
        private readonly IOrdersProvider _ordersProvider;
        private readonly IResponseGenerator _responseGenerator;

        public NorthwindReportHandler()
        {
            _paramsRetriever = new ParamsRetriever();
            _ordersProvider = new OrdersProvider();
            var xlsxGenerator = new XlsxReportGenerator();
            var xmlGenerator = new XmlReportGenerator();
            _responseGenerator = new ResponseGenerator(new List<IReportGenerator>{ xlsxGenerator, xmlGenerator });
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var filter = _paramsRetriever.GetParameters(context.Request);
            var orders = _ordersProvider.GetOrders(filter);
            _responseGenerator.GenerateResponse(context, orders);
        }
    }
}