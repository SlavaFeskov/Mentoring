using System.Data;
using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Infrastructure.Extensions;
using NorthwindDal.Infrastructure.Readers.Abstractions;

namespace NorthwindDal.Infrastructure.Readers.StoredProcedures
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