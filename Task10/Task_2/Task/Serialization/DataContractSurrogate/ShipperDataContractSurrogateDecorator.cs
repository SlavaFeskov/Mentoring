using System;
using Task.DB;
using Task.Serialization.DataContractSurrogate.Abstractions;

namespace Task.Serialization.DataContractSurrogate
{
    public class ShipperDataContractSurrogateDecorator : DataContractSurrogateDecorator
    {
        public ShipperDataContractSurrogateDecorator(Abstractions.DataContractSurrogate surrogate)
            : base(surrogate)
        {
        }

        public override Type GetDataContractType(Type type)
        {
            if (typeof(Supplier).IsAssignableFrom(type))
            {
                return typeof(Supplier);
            }

            return Surrogate.GetDataContractType(type);
        }

        public override object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Shipper shipper)
            {
                return new Shipper
                {
                    ShipperID = shipper.ShipperID,
                    CompanyName = shipper.CompanyName,
                    Phone = shipper.Phone
                };
            }

            return Surrogate.GetObjectToSerialize(obj, targetType);
        }
    }
}