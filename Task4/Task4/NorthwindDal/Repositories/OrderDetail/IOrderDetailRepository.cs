using System.Collections.Generic;
using NorthwindDal.Models.OrderDetail;

namespace NorthwindDal.Repositories.OrderDetail
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailModel> GetByOrderId(int orderId);
    }
}