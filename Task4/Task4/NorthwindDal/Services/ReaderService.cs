using NorthwindDal.Models.Order;
using NorthwindDal.Models.OrderDetail;
using NorthwindDal.Readers.Abstractions;
using NorthwindDal.Readers.Order;
using NorthwindDal.Readers.OrderDetail;
using NorthwindDal.Readers.StoredProcedures;
using NorthwindDal.Services.Abstractions;

namespace NorthwindDal.Services
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