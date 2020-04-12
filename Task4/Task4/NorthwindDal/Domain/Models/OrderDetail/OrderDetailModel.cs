// ReSharper disable InconsistentNaming

using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Domain.Models.Product;

namespace NorthwindDal.Domain.Models.OrderDetail
{
    public class OrderDetailModel
    {
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public OrderModel OrderModel { get; set; }

        public ProductModel ProductModel { get; set; }
    }
}