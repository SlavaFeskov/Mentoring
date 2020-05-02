using Autofac;
using MvcMusicStore.Infrastructure.Logging;

namespace MvcMusicStore.Infrastructure.Container.Modules
{
    public class GeneralModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logger>().As<ILogger>();
        }
    }
}