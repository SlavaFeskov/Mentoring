using System.Data.Common;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Order.StoredProceduresModels;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Readers.StoredProcedures
{
    public class CustomerOrderHistoryReader : BaseReader<CustomerOrderHistory>
    {
        public override CustomerOrderHistory ReadSingleWithOffset(DbDataReader reader, int offset)
        {
            var customerOrderHistoryRecord = new CustomerOrderHistory();
            customerOrderHistoryRecord.ProductName = reader.GetValueOrDefault<string>(0 + offset);
            customerOrderHistoryRecord.Quantity = reader.GetValueOrDefault<int>(1 + offset);
            return customerOrderHistoryRecord;
        }
    }
}