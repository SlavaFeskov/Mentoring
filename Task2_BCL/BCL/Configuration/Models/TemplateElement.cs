using System.Configuration;

namespace BCL.Configuration.Models
{
    public class TemplateElement : ConfigurationElement
    {
        [ConfigurationProperty("regex", IsKey = true)]
        public string FileRegex => (string) this["regex"];

        [ConfigurationProperty("destinationDirectory")]
        public string DestinationDirectory => (string) this["destinationDirectory"];

        [ConfigurationProperty("addIndex")] public bool AddIndex => (bool) this["addIndex"];

        [ConfigurationProperty("addDate")] public bool AddDate => (bool) this["addDate"];

        [ConfigurationProperty("dateFormat")] public string DateFormat => (string) this["dateFormat"];

        public override string ToString()
        {
            return $"{FileRegex} - {DestinationDirectory}";
        }
    }
}