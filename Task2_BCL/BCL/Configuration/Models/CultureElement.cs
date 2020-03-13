using System.Configuration;

namespace BCL.Configuration.Models
{
    public class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("cultureName")] public string CultureName => (string) this["cultureName"];
    }
}