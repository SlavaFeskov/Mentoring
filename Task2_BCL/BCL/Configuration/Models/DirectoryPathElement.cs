using System.Configuration;

namespace BCL.Configuration.Models
{
    public class DirectoryPathElement : ConfigurationElement
    {
        [ConfigurationProperty("directoryPath", IsKey = true)]
        public string DirectoryPath => (string) this["directoryPath"];
    }
}