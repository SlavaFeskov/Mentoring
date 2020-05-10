using System;
using System.Threading;
using CachingSolutionsSamples.Infrastructure.Abstractions;

namespace CachingSolutionsSamples.Fibonacci
{
    public class FibonacciCalculator
    {
        private readonly ICache<long?> _cache;

        public FibonacciCalculator(ICache<long?> cache)
        {
            _cache = cache;
        }

        public long Calculate(int number)
        {
            if (number == 0)
            {
                return 1;
            }

            if (number < 0)
            {
                throw new ArgumentException("Can't calculate Fibonacci value for negative number.");
            }

            var user = Thread.CurrentPrincipal.Identity.Name;
            var cachedValue = _cache.Get(user, number.ToString());
            if (cachedValue != null)
            {
                return cachedValue.Value;
            }

            var currentValue = Calculate(number - 1) * number;
            _cache.Set(currentValue, user, number.ToString());

            return currentValue;
        }
    }
}