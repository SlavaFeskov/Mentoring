using System.Collections.Generic;

namespace NorthwindDal.Domain.Models.Order
{
    public interface IOrderRepository
    {
        IEnumerable<OrderModel> GetAll();

        OrderModel Add(OrderModel orderModel);

        void Update(OrderModel order);

        OrderModel GetById(int orderId);

        void Delete(int orderId);

        IEnumerable<CustomerOrderHistory> GetCustomerOrderHistories(int customerId);

        IEnumerable<CustomerOrdersDetail> GetCustomerOrdersDetails(int orderId);
    }
}