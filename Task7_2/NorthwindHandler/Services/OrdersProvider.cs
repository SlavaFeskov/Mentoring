using System.Collections.Generic;
using System.Linq;
using NorthwindHandler.Models;
using NorthwindHandler.Services.Abstractions;
using Task5_EF.DbContext;

namespace NorthwindHandler.Services
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly NorthwindDbContext _context;

        public OrdersProvider()
        {
            _context = new NorthwindDbContext();
        }

        public IEnumerable<OrderModel> GetOrders(Filter filter)
        {
            var filteredOrders = _context.Orders
                .Where(filter.IsSatisfied)
                .Select(o => new OrderModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    CustomerID = o.CustomerID,
                    ShippedDate = o.ShippedDate,
                    RequiredDate = o.RequiredDate
                }).Skip(filter.Skip ?? 0).OrderBy(o => o.OrderID);
            return filter.Take != null ? filteredOrders.Take(filter.Take.Value) : filteredOrders;
        }
    }
}