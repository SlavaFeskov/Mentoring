using System;
using System.Collections.Generic;
using System.Linq;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Regions
{
    internal class RegionsManager : DbManager<Region>
    {
        public RegionsManager(ICache<IEnumerable<Region>> cache) : base(cache)
        {
        }

        protected override IEnumerable<Region> GetFromDb(Northwind dbContext) => dbContext.Regions.ToList();
    }
}