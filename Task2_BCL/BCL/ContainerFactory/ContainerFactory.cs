using Autofac;
using BCL.ContainerFactory.Modules;

namespace BCL.ContainerFactory
{
    public static class ContainerFactory
    {
        private static IContainer _container;

        private static readonly object LockObject = new object();

        public static IContainer GetContainer()
        {
            if (_container == null)
            {
                lock (LockObject)
                {
                    if (_container == null)
                    {
                        var builder = new ContainerBuilder();
                        builder.RegisterModule(new GeneralModule());
                        _container = builder.Build();
                    }
                }
            }

            return _container;
        }
    }
}