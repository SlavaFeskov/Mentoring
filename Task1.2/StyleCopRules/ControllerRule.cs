using System.Web.Mvc;
using StyleCop;
using StyleCop.CSharp;

namespace StyleCopRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class ControllerRule : BaseRule
    {
        protected override bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType == ElementType.Class)
            {
                if (element.Declaration.Name.Contains(nameof(Controller)))
                {
                    if (!element.Name.EndsWith(nameof(Controller)))
                    {
                        AddViolation(element, "ControllerInheritanceRule");
                    }
                }
            }
            return true;
        }
    }
}