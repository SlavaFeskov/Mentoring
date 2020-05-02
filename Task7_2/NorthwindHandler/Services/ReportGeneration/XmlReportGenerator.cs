using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions.ReportGeneration;

namespace NorthwindHandler.Services.ReportGeneration
{
    public class XmlReportGenerator : IReportGenerator
    {
        public IEnumerable<string> ContentTypes => new List<string>{ "text/xml", "application/xml" };
        public ReportFormat Format => ReportFormat.Xml;

        public Stream Generate(IEnumerable<OrderModel> orders)
        {
            var memoryStream = new MemoryStream();
            var document = new XmlDocument();
            XmlNode rootElement = document.CreateElement("Orders");
            foreach (var order in orders)
            {
                rootElement.AppendChild(CreateOrderElement(document, order));
            }

            document.AppendChild(rootElement);
            document.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        private XmlNode CreateOrderElement(XmlDocument document, OrderModel order)
        {
            XmlNode orderElement = document.CreateElement("Order");
            var orderProperties = order.GetType().GetProperties();
            foreach (var orderProperty in orderProperties)
            {
                var orderPropertyElement = document.CreateElement(orderProperty.Name);
                var propValue = orderProperty.GetValue(order);
                if (propValue is DateTime propValueAsDateTime)
                {
                    orderPropertyElement.InnerText = propValueAsDateTime.ToShortDateString();
                }
                else
                {
                    orderPropertyElement.InnerText = propValue.ToString();
                }

                orderElement.AppendChild(orderPropertyElement);
            }

            return orderElement;
        }
    }
}