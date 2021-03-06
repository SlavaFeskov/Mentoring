﻿using System.Data;
using NorthwindDal.Domain.Models.Order;
using NorthwindDal.Infrastructure.Extensions;
using NorthwindDal.Infrastructure.Readers.Abstractions;

namespace NorthwindDal.Infrastructure.Readers.StoredProcedures
{
    public class CustomerOrdersDetailReader : BaseReader<CustomerOrdersDetail>
    {
        public override CustomerOrdersDetail ReadSingle(IDataReader reader)
        {
            var customerOrderDetailRecord = new CustomerOrdersDetail();
            customerOrderDetailRecord.ProductName =
                reader.GetValueOrDefault<string>(nameof(CustomerOrdersDetail.ProductName));
            customerOrderDetailRecord.UnitPrice =
                reader.GetValueOrDefault<float>(nameof(CustomerOrdersDetail.UnitPrice));
            customerOrderDetailRecord.Quantity = reader.GetValueOrDefault<int>(nameof(CustomerOrdersDetail.Quantity));
            customerOrderDetailRecord.Discount = reader.GetValueOrDefault<int>(nameof(CustomerOrdersDetail.Discount));
            customerOrderDetailRecord.ExtendedPrice =
                reader.GetValueOrDefault<decimal>(nameof(CustomerOrdersDetail.ExtendedPrice));
            return customerOrderDetailRecord;
        }
    }
}