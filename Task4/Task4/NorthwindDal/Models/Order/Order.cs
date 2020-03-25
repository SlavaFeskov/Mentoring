using System;

// ReSharper disable InconsistentNaming

namespace NorthwindDal.Models.Order
{
    public class Order
    {
        public int OrderID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public int? ShipVia { get; set; }

        public OrderState OrderState
        {
            get
            {
                if (OrderDate == null)
                {
                    return OrderState.New;
                }

                if (ShippedDate == null)
                {
                    return OrderState.InProgress;
                }
                else
                {
                    return OrderState.Completed;
                }
            }
        }
    }
}