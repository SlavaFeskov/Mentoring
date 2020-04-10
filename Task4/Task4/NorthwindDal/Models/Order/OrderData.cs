using System.Collections.Generic;
using System.Linq;

namespace NorthwindDal.Models.Order
{
    public static class OrderData
    {
        public static IEnumerable<string> GetPropertyNames()
            => typeof(OrderModel).GetProperties().Select(p => p.Name);
    }
}