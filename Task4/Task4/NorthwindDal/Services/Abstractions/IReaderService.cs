using NorthwindDal.Models.Order;
using NorthwindDal.Models.OrderDetail;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Services.Abstractions
{
    public interface IReaderService
    {
        IReader<OrderModel> OrderReader { get; set; }
        IReader<OrderDetailModel> OrderDetailReader { get; set; }
        IReader<CustomerOrderHistory> CustomerOrderHistoryReader { get; set; }
        IReader<CustomerOrdersDetail> CustomerOrderDetailsReader { get; set; }
    }
}