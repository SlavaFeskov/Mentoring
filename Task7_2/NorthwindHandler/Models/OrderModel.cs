// ReSharper disable InconsistentNaming

using System;

namespace NorthwindHandler.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public string CustomerID { get; set; }
    }
}