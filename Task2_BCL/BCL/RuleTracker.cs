using System.Text.RegularExpressions;
using BCL.Configuration;
using BCL.Configuration.Models;

namespace BCL
{
    public static class RuleTracker
    {
        public static TemplateCollection Rules = ConfigFactory.GetGeneralSection().Templates;

        public static TemplateElement CheckRulesForFile(string fileName)
        {
            foreach (TemplateElement rule in Rules)
            {
                if (Regex.IsMatch(fileName, rule.FileRegex))
                {
                    return rule;
                }
            }

            return null;
        }
    }
}