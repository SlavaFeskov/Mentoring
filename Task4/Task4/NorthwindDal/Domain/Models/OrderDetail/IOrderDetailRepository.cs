using System.Collections.Generic;

namespace NorthwindDal.Domain.Models.OrderDetail
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailModel> GetByOrderId(int orderId);
    }
}