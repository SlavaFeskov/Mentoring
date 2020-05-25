using System;
using System.Linq;
using System.Threading;
using CachingSolutionsSamples.Infrastructure.Categories;
using CachingSolutionsSamples.Infrastructure.Customers;
using CachingSolutionsSamples.Infrastructure.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingSolutionsSamples.Tests
{
    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void MemoryCacheCategories()
        {
            var categoryManager =
                new CategoriesManager(
                    new CategoriesMemoryCache(new PolicyCreator("SELECT CategoryID FROM [dbo].[Categories]")));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetAll().Count());
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void RedisCacheCategories()
        {
            var categoriesCache = new CategoriesRedisCache(cacheExpiry: TimeSpan.FromSeconds(5));
            categoriesCache.ClearCache(Thread.CurrentPrincipal.Identity.Name);
            var categoryManager = new CategoriesManager(categoriesCache);

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetAll().Count());
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void MemoryCacheCustomers()
        {
            var customersManager =
                new CustomersManager(
                    new CustomersMemoryCache(new PolicyCreator("SELECT CustomerID FROM [dbo].[Customers]")));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(customersManager.GetAll().Count());
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void RedisCacheCustomers()
        {
            var customersCache = new CustomersRedisCache(cacheExpiry: TimeSpan.FromSeconds(5));
            customersCache.ClearCache(Thread.CurrentPrincipal.Identity.Name);
            var customersManager = new CustomersManager(customersCache);

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(customersManager.GetAll().Count());
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void MemoryCacheRegions()
        {
            var regionsManager =
                new RegionsManager(
                    new RegionsMemoryCache(new PolicyCreator("SELECT RegionID FROM [dbo].[Region]")));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(regionsManager.GetAll().Count());
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void RedisCacheRegions()
        {
            var regionsRedisCache = new RegionsRedisCache(cacheExpiry: TimeSpan.FromSeconds(5));
            regionsRedisCache.ClearCache(Thread.CurrentPrincipal.Identity.Name);
            var regionsManager = new RegionsManager(regionsRedisCache);

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(regionsManager.GetAll().Count());
                Thread.Sleep(2000);
            }
        }
    }
}