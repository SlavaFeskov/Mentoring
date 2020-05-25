using System.Collections.Generic;
using System.Linq;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Customers
{
    internal class CustomersManager : DbManager<Customer>
    {
        public CustomersManager(ICache<IEnumerable<Customer>> cache) : base(cache)
        {
        }

        protected override IEnumerable<Customer> GetFromDb(Northwind dbContext) => dbContext.Customers.ToList();
    }
}