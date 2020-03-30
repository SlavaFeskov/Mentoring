using System;
using System.Text.RegularExpressions;
using BCL.Configuration;
using BCL.Configuration.Models;
using BCL.Resources;
using BCL.Rules.Abstractions;

namespace BCL.Rules
{
    public class RuleValidator : IRuleValidator
    {
        private readonly TemplateCollection _rules = ConfigFactory.GetGeneralSection().Templates;

        public event EventHandler<FileWatcherEventArgs> RuleFoundNotFound;

        public TemplateElement CheckForRuleToApply(string fileName)
        {
            foreach (TemplateElement rule in _rules)
            {
                if (Regex.IsMatch(fileName, rule.FileRegex))
                {
                    OnRuleFoundNotFound(new FileWatcherEventArgs
                        {Message = string.Format(StringResources.RuleWasFound, rule, fileName)});
                    return rule;
                }
            }

            OnRuleFoundNotFound(new FileWatcherEventArgs
                {Message = string.Format(StringResources.RuleWasNotFound, fileName)});
            return null;
        }

        protected virtual void OnRuleFoundNotFound(FileWatcherEventArgs e)
        {
            RuleFoundNotFound?.Invoke(this, e);
        }
    }
}