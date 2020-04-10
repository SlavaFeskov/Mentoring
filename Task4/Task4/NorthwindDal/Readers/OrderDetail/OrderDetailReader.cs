using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Order;
using NorthwindDal.Models.OrderDetail;
using NorthwindDal.Models.Product;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Readers.OrderDetail
{
    public class OrderDetailReader : BaseReader<OrderDetailModel>
    {
        private readonly IReader<OrderModel> _orderReader;

        public OrderDetailReader(IReader<OrderModel> orderReader) : base()
        {
            _orderReader = orderReader;
        }

        public override OrderDetailModel ReadSingleWithOffset(IDataReader reader, int offset)
        {
            var orderDetail = new OrderDetailModel();
            orderDetail.ProductID = reader.GetValueOrDefault<int>(0 + offset);
            orderDetail.UnitPrice = reader.GetValueOrDefault<decimal>(1 + offset);
            orderDetail.Quantity = reader.GetValueOrDefault<short>(2 + offset);
            orderDetail.Discount = reader.GetValueOrDefault<float>(3 + offset);
            orderDetail.OrderModel = _orderReader.ReadSingleWithOffset(reader, 4 + offset);
            orderDetail.OrderID = orderDetail.OrderModel.OrderID;
            orderDetail.ProductModel = new ProductModel();
            orderDetail.ProductModel.ProductID = orderDetail.ProductID;
            orderDetail.ProductModel.ProductName = reader.GetValueOrDefault<string>(18 + offset);
            return orderDetail;
        }
    }
}