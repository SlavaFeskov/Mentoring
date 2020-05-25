namespace CachingSolutionsSamples.Infrastructure.Abstractions
{
    public interface ICache<TCacheValue>
    {
        TCacheValue Get(string forUser, string identifier = "");

        void Set(TCacheValue cacheValue, string forUser, string identifier = "");
    }
}