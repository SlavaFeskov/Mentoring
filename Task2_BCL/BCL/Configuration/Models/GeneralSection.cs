using System.Configuration;

namespace BCL.Configuration.Models
{
    public class GeneralSection : ConfigurationSection
    {
        [ConfigurationProperty("culture")] public CultureElement Culture => (CultureElement) this["culture"];

        [ConfigurationCollection(typeof(DirectoryPathElement), AddItemName = "directory")]
        [ConfigurationProperty("directoriesToWatch")]
        public DirectoriesCollection DirectoriesToWatch => (DirectoriesCollection) this["directoriesToWatch"];

        [ConfigurationCollection(typeof(TemplateElement), AddItemName = "template")]
        [ConfigurationProperty("templates")]
        public TemplateCollection Templates => (TemplateCollection) this["templates"];
    }
}