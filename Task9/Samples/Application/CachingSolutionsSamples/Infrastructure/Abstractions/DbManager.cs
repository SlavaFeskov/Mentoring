using System;
using System.Collections.Generic;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Abstractions
{
    public abstract class DbManager<TEntity>
    {
        private readonly ICache<IEnumerable<TEntity>> _cache;

        protected DbManager(ICache<IEnumerable<TEntity>> cache)
        {
            _cache = cache;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = _cache.Get(user);

            if (entities == null)
            {
                Console.Write("From DB: ");

                using var dbContext = new Northwind();
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                entities = GetFromDb(dbContext);
                _cache.Set(entities, user);
            }
            else
            {
                Console.Write("From Cache: ");
            }

            return entities;
        }

        protected abstract IEnumerable<TEntity> GetFromDb(Northwind dbContext);
    }
}