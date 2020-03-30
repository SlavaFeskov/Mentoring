using System;
using BCL.Configuration.Models;

namespace BCL.Rules.Abstractions
{
    public interface IRuleValidator
    {
        event EventHandler<FileWatcherEventArgs> RuleFoundNotFound;
        TemplateElement CheckForRuleToApply(string fileName);
    }
}