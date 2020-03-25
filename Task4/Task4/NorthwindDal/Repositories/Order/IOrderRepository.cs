using System;
using System.Collections.Generic;
using NorthwindDal.Models.Dto;
using NorthwindDal.Models.Order.StoredProceduresModels;

namespace NorthwindDal.Repositories.Order
{
    public interface IOrderRepository
    {
        IEnumerable<Models.Order.Order> GetOrders();

        Models.Order.Order Add(Models.Order.Order order);

        int Update(OrderDto order);

        int Update(int orderId, IDictionary<string, object> values);

        Models.Order.Order GetOrderById(int orderId);

        int Delete(int orderId);

        int TakeOrderInProgress(int orderId, DateTime orderDate);

        int CompleteOrder(int orderId, DateTime shippedDate);

        IEnumerable<CustomerOrderHistory> GetCustomerOrderHistories(int customerId);

        IEnumerable<CustomerOrdersDetail> GetCustomerOrdersDetails(int orderId);
    }
}