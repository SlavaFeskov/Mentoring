using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Domain.Models.OrderDetail;
using NorthwindDal.Infrastructure.Readers.Abstractions;
using NorthwindDal.Infrastructure.Readers.Order;
using NorthwindDal.Infrastructure.Readers.OrderDetail;
using NorthwindDal.Infrastructure.Readers.StoredProcedures;
using NorthwindDal.Infrastructure.Services.Abstractions;

namespace NorthwindDal.Infrastructure.Services
{
    public class ReaderService : IReaderService
    {
        public IReader<OrderModel> OrderReader { get; set; }

        public IReader<OrderDetailModel> OrderDetailReader { get; set; }

        public IReader<CustomerOrderHistory> CustomerOrderHistoryReader { get; set; }

        public IReader<CustomerOrdersDetail> CustomerOrderDetailsReader { get; set; }

        public ReaderService()
        {
            OrderReader = new OrderReader();
            OrderDetailReader = new OrderDetailReader(OrderReader);
            CustomerOrderHistoryReader = new CustomerOrderHistoryReader();
            CustomerOrderDetailsReader = new CustomerOrdersDetailReader();
        }
    }
}