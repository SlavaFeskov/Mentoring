using System.Collections.Generic;
using NorthwindDal.Models.Order;

namespace NorthwindDal.Repositories.Order
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