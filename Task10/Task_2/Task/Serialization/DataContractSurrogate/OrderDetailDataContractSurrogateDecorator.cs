using System;
using Task.DB;
using Task.Serialization.DataContractSurrogate.Abstractions;

namespace Task.Serialization.DataContractSurrogate
{
    public class OrderDetailDataContractSurrogateDecorator : DataContractSurrogateDecorator
    {
        public OrderDetailDataContractSurrogateDecorator(Abstractions.DataContractSurrogate surrogate)
            : base(surrogate)
        {
        }

        public override Type GetDataContractType(Type type)
        {
            if (typeof(Order_Detail).IsAssignableFrom(type))
            {
                return typeof(Order_Detail);
            }

            return Surrogate.GetDataContractType(type);
        }

        public override object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Order_Detail orderDetail)
            {
                return new Order_Detail
                {
                    OrderID = orderDetail.OrderID,
                    ProductID = orderDetail.ProductID,
                    UnitPrice = orderDetail.UnitPrice,
                    Quantity = orderDetail.Quantity,
                    Discount = orderDetail.Discount
                };
            }

            return Surrogate.GetObjectToSerialize(obj, targetType);
        }
    }
}