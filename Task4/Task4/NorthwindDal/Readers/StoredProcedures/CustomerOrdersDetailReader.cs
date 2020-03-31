using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Order.StoredProceduresModels;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Readers.StoredProcedures
{
    public class CustomerOrdersDetailReader : BaseReader<CustomerOrdersDetail>
    {
        public override CustomerOrdersDetail ReadSingleWithOffset(IDataReader reader, int offset)
        {
            var customerOrderDetailRecord = new CustomerOrdersDetail();
            customerOrderDetailRecord.ProductName = reader.GetValueOrDefault<string>(0 + offset);
            customerOrderDetailRecord.UnitPrice = reader.GetValueOrDefault<float>(1 + offset);
            customerOrderDetailRecord.Quantity = reader.GetValueOrDefault<int>(2 + offset);
            customerOrderDetailRecord.Discount = reader.GetValueOrDefault<int>(3 + offset);
            customerOrderDetailRecord.ExtendedPrice = reader.GetValueOrDefault<decimal>(4 + offset);
            return customerOrderDetailRecord;
        }
    }
}