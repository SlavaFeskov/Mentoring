using System.Configuration;

namespace BCL.Configuration.Models
{
    public class TemplateCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDirectory")]
        public string DefaultDirectory => (string)this["defaultDirectory"];

        protected override ConfigurationElement CreateNewElement() => new TemplateElement();

        protected override object GetElementKey(ConfigurationElement element) => ((TemplateElement) element).FileRegex;
    }
}