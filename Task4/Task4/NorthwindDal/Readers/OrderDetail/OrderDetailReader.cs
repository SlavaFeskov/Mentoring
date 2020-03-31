using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Models;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Readers.OrderDetail
{
    public class OrderDetailReader : BaseReader<Models.OrderDetail.OrderDetail>
    {
        private readonly IReader<Models.Order.Order> _orderReader;

        public OrderDetailReader(IReader<Models.Order.Order> orderReader)
        {
            _orderReader = orderReader;
        }

        public override Models.OrderDetail.OrderDetail ReadSingleWithOffset(IDataReader reader, int offset)
        {
            var orderDetail = new Models.OrderDetail.OrderDetail();
            orderDetail.ProductID = reader.GetValueOrDefault<int>(0 + offset);
            orderDetail.UnitPrice = reader.GetValueOrDefault<decimal>(1 + offset);
            orderDetail.Quantity = reader.GetValueOrDefault<short>(2 + offset);
            orderDetail.Discount = reader.GetValueOrDefault<float>(3 + offset);
            orderDetail.Order = _orderReader.ReadSingleWithOffset(reader, 4 + offset);
            orderDetail.OrderID = orderDetail.Order.OrderID;
            orderDetail.Product = new Product();
            orderDetail.Product.ProductID = orderDetail.ProductID;
            orderDetail.Product.ProductName = reader.GetValueOrDefault<string>(18 + offset);
            return orderDetail;
        }
    }
}