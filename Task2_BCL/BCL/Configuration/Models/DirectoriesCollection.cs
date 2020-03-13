using System;
using System.Configuration;

namespace BCL.Configuration.Models
{
    public class DirectoriesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new DirectoryPathElement();

        protected override object GetElementKey(ConfigurationElement element) =>
            ((DirectoryPathElement) element).DirectoryPath;
    }
}