using System.Collections.Generic;
using System.Linq;
using NorthwindDal.Domain.Models.Order;

namespace NorthwindDal.Infrastructure.Repositories.Order
{
    public static class OrderData
    {
        public static IEnumerable<string> GetPropertyNames()
            => typeof(OrderModel).GetProperties().Select(p => p.Name);
    }
}