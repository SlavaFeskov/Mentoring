using System;
using System.IO;
using Autofac;
using FileSystemVisitorLib_V2.Models;
using FileSystemVisitorLib_V2.Services;
using FileSystemVisitorLib_V2.Services.Abstractions;

namespace FileSystemVisitorLib_V2.Container.Modules
{
    public class GeneralModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) => new FileSystemVisitor2(c.Resolve<IFileSystemObjectsProvider<FileSystemInfo>>(),
                    c.Resolve<IFiltrator<FileSystemInfo>>(), p.Named<Func<BaseObject<FileSystemInfo>, bool>>("filter")))
                .As<IFileSystemVisitor<FileSystemInfo>>();
            builder.RegisterType<FileSystemObjectProvider>().As<IFileSystemObjectsProvider<FileSystemInfo>>();
            builder.RegisterType<FileSystemObjectFiltrator>().As<IFiltrator<FileSystemInfo>>();
        }
    }
}