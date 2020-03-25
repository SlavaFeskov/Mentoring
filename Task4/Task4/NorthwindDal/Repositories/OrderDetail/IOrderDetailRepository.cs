using System.Collections.Generic;

namespace NorthwindDal.Repositories.OrderDetail
{
    public interface IOrderDetailRepository
    {
        IEnumerable<Models.OrderDetail.OrderDetail> GetOrderDetailsByOrderId(int orderId);
    }
}