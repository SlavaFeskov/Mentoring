using System.Linq;
using System.Web.Mvc;
using StyleCop;
using StyleCop.CSharp;

namespace StyleCopRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class AuthorizeRule : BaseRule
    {
        protected override bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType == ElementType.Class)
            {
                if (element.Declaration.Name.Contains(nameof(Controller)))
                {
                    if (!element.Attributes.Select(a => a.GetType()).Contains(typeof(AuthorizeAttribute)))
                    {
                        var hasNotAuthorizedMethod = false;
                        var publicMethods = element.ChildElements.Where(e =>
                            e.AccessModifier == AccessModifierType.Public && e.ElementType == ElementType.Method);
                        foreach (var publicMethod in publicMethods)
                        {
                            if (!publicMethod.Attributes.Select(a => a.GetType()).Contains(typeof(AuthorizeAttribute)))
                            {
                                AddViolation(publicMethod, "AuthorizePublicMethodRule",
                                    "AllPublicMethodsShouldHaveAuthorizeAttribute");
                                hasNotAuthorizedMethod = true;
                            }
                        }

                        if (hasNotAuthorizedMethod)
                        {
                            AddViolation(element, "AuthorizeClassRule");
                        }
                    }
                }
            }

            return true;
        }
    }
}