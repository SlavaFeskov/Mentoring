using NorthwindDal.Models.Order;
using NorthwindDal.Models.Order.StoredProceduresModels;
using NorthwindDal.Models.OrderDetail;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Services.Abstractions
{
    public interface IReaderService
    {
        IReader<Order> OrderReader { get; set; }
        IReader<OrderDetail> OrderDetailReader { get; set; }
        IReader<CustomerOrderHistory> CustomerOrderHistoryReader { get; set; }
        IReader<CustomerOrdersDetail> CustomerOrderDetailsReader { get; set; }
    }
}