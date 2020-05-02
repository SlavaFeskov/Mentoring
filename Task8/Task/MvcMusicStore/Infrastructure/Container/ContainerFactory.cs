using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Infrastructure.Container.Modules;

namespace MvcMusicStore.Infrastructure.Container
{
    public class ContainerFactory
    {
        private static IContainer _container;
        private static readonly object LockObject = new object();

        public static IContainer Create()
        {
            if (_container == null)
            {
                lock (LockObject)
                {
                    if (_container == null)
                    {
                        var builder = new ContainerBuilder();
                        builder.RegisterModule<GeneralModule>();
                        builder.RegisterControllers(typeof(HomeController).Assembly);
                        _container = builder.Build();
                    }
                }
            }

            return _container;
        }
    }
}