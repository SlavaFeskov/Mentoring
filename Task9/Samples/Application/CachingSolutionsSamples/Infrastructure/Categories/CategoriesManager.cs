using System.Collections.Generic;
using System.Linq;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Categories
{
    internal class CategoriesManager : DbManager<Category>
    {
        public CategoriesManager(ICache<IEnumerable<Category>> cache) : base(cache)
        {
        }

        protected override IEnumerable<Category> GetFromDb(Northwind dbContext) => dbContext.Categories.ToList();
    }
}