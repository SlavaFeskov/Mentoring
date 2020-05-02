using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Domain.Models.OrderDetail;
using NorthwindDal.Infrastructure.Readers.Abstractions;

namespace NorthwindDal.Infrastructure.Services.Abstractions
{
    public interface IReaderService
    {
        IReader<OrderModel> OrderReader { get; set; }
        IReader<OrderDetailModel> OrderDetailReader { get; set; }
        IReader<CustomerOrderHistory> CustomerOrderHistoryReader { get; set; }
        IReader<CustomerOrdersDetail> CustomerOrderDetailsReader { get; set; }
    }
}