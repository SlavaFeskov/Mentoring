using System;
using Domain.Validation.Abstractions;
using NorthwindDal.Models.Order;

namespace Domain.Validation
{
    public class OrderValidator : IValidator<OrderModel>
    {
        public void Validate(OrderModel model)
        {
            ValidateOrderInProgressOrCompletedStatusCannotBeUpdated(model);
                
            if (values.ContainsKey("OrderDate") || values.ContainsKey("ShippedDate"))
            {
                throw new InvalidOperationException("Cannot explicitly set OrderDate or ShippedDate for order.");
            }
        }

        private void ValidateOrderInProgressOrCompletedStatusCannotBeUpdated(OrderModel model)
        {
            if (model.OrderState == OrderState.InProgress || model.OrderState == OrderState.Completed)
            {
                throw new InvalidOperationException("Cannot update an order with InProgress or Completed status.");
            }
        }

        private void ValidateOrderDateOrShippedDateCannotBeUpdatedExplicitly()
    }
}