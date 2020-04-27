using System;

namespace NorthwindHandler.Models
{
    public class Filter
    {
        public string CustomerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }

        public bool IsSatisfied(OrderModel order)
        {
            var result = true;
            if (CustomerId != null)
            {
                result = order.CustomerID == CustomerId;
            }

            if (DateFrom != null)
            {
                result = order.OrderDate >= DateFrom;
            }

            if (DateTo != null)
            {
                result = order.OrderDate <= DateTo;
            }

            return result;
        }
    }
}