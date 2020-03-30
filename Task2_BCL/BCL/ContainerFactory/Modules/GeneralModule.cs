using Autofac;
using BCL.Rules;
using BCL.Rules.Abstractions;
using BCL.Services;
using BCL.Services.Abstractions;

namespace BCL.ContainerFactory.Modules
{
    public class GeneralModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileWorker>().As<IFileWorker>();
            builder.RegisterType<WatchingService>().As<IWatchingService>();
            builder.RegisterType<RuleValidator>().As<IRuleValidator>();
            builder.RegisterType<FileSystemWatcher>().As<IWatcher>();
        }
    }
}