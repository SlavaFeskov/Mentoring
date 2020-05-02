using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using log4net.Config;
using MvcMusicStore.Infrastructure.Container;
using MvcMusicStore.Infrastructure.Performance;

namespace MvcMusicStore
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.Configure();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(ContainerFactory.Create()));

            var counterHelper = PerformanceCounterFactory.Create();
            counterHelper.RawValue(Counters.SuccessfulLogIn, 0);
            counterHelper.RawValue(Counters.SuccessfulLogOff, 0);
            counterHelper.RawValue(Counters.Checkout, 0);
        }
    }
}