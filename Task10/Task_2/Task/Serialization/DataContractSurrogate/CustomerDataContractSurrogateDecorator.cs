using System;
using Task.DB;
using Task.Serialization.DataContractSurrogate.Abstractions;

namespace Task.Serialization.DataContractSurrogate
{
    public class CustomerDataContractSurrogateDecorator : DataContractSurrogateDecorator
    {
        public CustomerDataContractSurrogateDecorator(Abstractions.DataContractSurrogate surrogate)
            : base(surrogate)
        {
        }

        public override Type GetDataContractType(Type type)
        {
            if (typeof(Customer).IsAssignableFrom(type))
            {
                return typeof(Customer);
            }

            return Surrogate.GetDataContractType(type);
        }

        public override object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Customer customer)
            {
                return new Customer
                {
                    CustomerID = customer.CustomerID,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                };
            }

            return Surrogate.GetObjectToSerialize(obj, targetType);
        }
    }
}