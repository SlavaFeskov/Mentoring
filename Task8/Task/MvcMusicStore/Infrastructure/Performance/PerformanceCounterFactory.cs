using PerformanceCounterHelper;

namespace MvcMusicStore.Infrastructure.Performance
{
    public class PerformanceCounterFactory
    {
        private static CounterHelper<Counters> _counterHelper;

        public static CounterHelper<Counters> Create()
        {
            return _counterHelper ??= PerformanceHelper.CreateCounterHelper<Counters>("MvcMusicStoreCounter");
        }
    }
}