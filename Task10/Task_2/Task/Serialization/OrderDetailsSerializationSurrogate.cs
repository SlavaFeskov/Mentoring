using System.Linq;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Serialization
{
    public class OrderDetailsSerializationSurrogate : ISerializationSurrogate
    {
        private readonly Northwind _dbContext;

        public OrderDetailsSerializationSurrogate(Northwind dbContext)
        {
            _dbContext = dbContext;
        }

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail) obj;
            info.AddValue(nameof(Order_Detail.OrderID), orderDetail.OrderID);
            info.AddValue(nameof(Order_Detail.ProductID), orderDetail.ProductID);
            info.AddValue(nameof(Order_Detail.UnitPrice), orderDetail.UnitPrice);
            info.AddValue(nameof(Order_Detail.Quantity), orderDetail.Quantity);
            info.AddValue(nameof(Order_Detail.Discount), orderDetail.Discount);
            info.AddValue(nameof(Order_Detail.Order), _dbContext.Orders.Single(o => o.OrderID == orderDetail.OrderID));
            info.AddValue(nameof(Order_Detail.Product),
                _dbContext.Products.Single(p => p.ProductID == orderDetail.ProductID));
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail) obj;
            orderDetail.OrderID = info.GetInt32(nameof(Order_Detail.OrderID));
            orderDetail.ProductID = info.GetInt32(nameof(Order_Detail.ProductID));
            orderDetail.UnitPrice = info.GetDecimal(nameof(Order_Detail.UnitPrice));
            orderDetail.Quantity = info.GetInt16(nameof(Order_Detail.Quantity));
            orderDetail.Discount = (float) info.GetValue(nameof(Order_Detail.Discount), typeof(float));
            orderDetail.Order = (Order) info.GetValue(nameof(Order_Detail.Order), typeof(Order));
            orderDetail.Product = (Product) info.GetValue(nameof(Order_Detail.Product), typeof(Product));

            return orderDetail;
        }
    }
}