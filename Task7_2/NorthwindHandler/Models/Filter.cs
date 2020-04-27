using System;
using System.Collections.Generic;
using System.Linq;  
using Task5_EF.Models;

namespace NorthwindHandler.Models
{
    public class Filter
    {
        public string CustomerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }

        public bool IsSatisfied(Order order)
        {
            var checks = new List<bool>();
            if (CustomerId != null)
            {
                checks.Add(order.CustomerID == CustomerId);
            }

            if (DateFrom != null)
            {
                checks.Add(order.OrderDate >= DateFrom);
            }

            if (DateTo != null)
            {
                checks.Add(order.OrderDate <= DateTo);
            }

            return checks.All(c => c);
        }
    }
}