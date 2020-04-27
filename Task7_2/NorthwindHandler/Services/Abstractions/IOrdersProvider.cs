using System.Collections.Generic;
using NorthwindHandler.Models;

namespace NorthwindHandler.Services.Abstractions
{
    public interface IOrdersProvider
    {
        IEnumerable<OrderModel> GetOrders(Filter filter);
    }
}