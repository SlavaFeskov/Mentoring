using System;

// ReSharper disable InconsistentNaming

namespace NorthwindDal.Domain.Models.Order
{
    public class OrderModel
    {
        public OrderModel(DateTime? orderDate = null, DateTime? shippedDate = null)
        {
            OrderDate = orderDate;
            ShippedDate = shippedDate;
        }

        public int OrderID { get; set; }

        public DateTime? OrderDate { get; private set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; private set; }

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

        public OrderState GetOrderState()
        {
            if (OrderDate == null)
            {
                return OrderState.New;
            }

            if (ShippedDate == null)
            {
                return OrderState.InProgress;
            }

            return OrderState.Completed;
        }

        public void CompleteOrder(DateTime shippedDate)
        {
            if (GetOrderState() == OrderState.InProgress)
            {
                ShippedDate = shippedDate;
            }
            else
            {
                throw new InvalidOperationException("Cannot complete order not InProgress state.");
            }
        }

        public void TakeInProgress(DateTime orderDate)
        {
            if (GetOrderState() == OrderState.New)
            {
                OrderDate = orderDate;
            }
            else
            {
                throw new InvalidOperationException("Cannot take in progress order not in New state.");
            }
        }

        public void ValidateOrderForDeletion()
        {
            if (GetOrderState() == OrderState.Completed)
            {
                throw new InvalidOperationException("Cannot delete order in Completed state.");
            }
        }

        public void ValidateOrderForUpdate()
        {
            var orderState = GetOrderState();
            if (orderState == OrderState.InProgress || orderState == OrderState.Completed)
            {
                throw new InvalidOperationException("Cannot update an order with InProgress or Completed status.");
            }
        }
    }
}