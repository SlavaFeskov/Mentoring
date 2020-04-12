using System.Data;
using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Domain.Models.OrderDetail;
using NorthwindDal.Domain.Models.Product;
using NorthwindDal.Infrastructure.Extensions;
using NorthwindDal.Infrastructure.Readers.Abstractions;

namespace NorthwindDal.Infrastructure.Readers.OrderDetail
{
    public class OrderDetailReader : BaseReader<OrderDetailModel>
    {
        private readonly IReader<OrderModel> _orderReader;

        public OrderDetailReader(IReader<OrderModel> orderReader)
        {
            _orderReader = orderReader;
        }

        public override OrderDetailModel ReadSingle(IDataReader reader)
        {
            var orderDetail = new OrderDetailModel();
            orderDetail.ProductID = reader.GetValueOrDefault<int>(nameof(OrderDetailModel.ProductID));
            orderDetail.UnitPrice = reader.GetValueOrDefault<decimal>(nameof(OrderDetailModel.UnitPrice));
            orderDetail.Quantity = reader.GetValueOrDefault<short>(nameof(OrderDetailModel.Quantity));
            orderDetail.Discount = reader.GetValueOrDefault<float>(nameof(OrderDetailModel.Discount));
            orderDetail.OrderModel = _orderReader.ReadSingle(reader);
            orderDetail.OrderID = orderDetail.OrderModel.OrderID;
            orderDetail.ProductModel = new ProductModel();
            orderDetail.ProductModel.ProductID = orderDetail.ProductID;
            orderDetail.ProductModel.ProductName = reader.GetValueOrDefault<string>(nameof(ProductModel.ProductName));
            return orderDetail;
        }
    }
}