using System.Diagnostics;
using PerformanceCounterHelper;

namespace MvcMusicStore.Infrastructure.Performance
{
    [PerformanceCounterCategory("MvcMusicStoreCategory", PerformanceCounterCategoryType.MultiInstance,
        "MvcMusicStoreCounter")]
    public enum Counters
    {
        [PerformanceCounter("LOGIN", "Successful LogIn counter", PerformanceCounterType.NumberOfItems32)]
        SuccessfulLogIn,

        [PerformanceCounter("LOGOFF", "Successful LogOff counter", PerformanceCounterType.NumberOfItems32)]
        SuccessfulLogOff,

        [PerformanceCounter("CHECKOUT", "Check out counter", PerformanceCounterType.NumberOfItems32)]
        Checkout
    }
}