using System.Data;
using NorthwindDal.Extensions;
using NorthwindDal.Models.Order;
using NorthwindDal.Readers.Abstractions;

namespace NorthwindDal.Readers.StoredProcedures
{
    public class CustomerOrderHistoryReader : BaseReader<CustomerOrderHistory>
    {
        public override CustomerOrderHistory ReadSingle(IDataReader reader)
        {
            var customerOrderHistoryRecord = new CustomerOrderHistory();
            customerOrderHistoryRecord.ProductName =
                reader.GetValueOrDefault<string>(nameof(CustomerOrderHistory.ProductName));
            customerOrderHistoryRecord.Quantity = reader.GetValueOrDefault<int>(nameof(CustomerOrderHistory.Quantity));
            return customerOrderHistoryRecord;
        }
    }
}