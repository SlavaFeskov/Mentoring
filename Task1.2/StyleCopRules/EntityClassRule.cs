using System.Linq;
using System.Runtime.Serialization;
using StyleCop.CSharp;

namespace StyleCopRules
{
    public class EntityClassRule : BaseRule
    {
        protected override bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType == ElementType.Namespace)
            {
                if (element.Name.EndsWith("Entities"))
                {
                    var classesInNamespace = element.ChildElements.Where(e =>
                        e.ElementType == ElementType.Class);
                    foreach (var classInNamespace in classesInNamespace)
                    {
                        if (classInNamespace.AccessModifier != AccessModifierType.Public)
                        {
                            AddViolation(classInNamespace, "EntityClassMustBePublicRule");
                        }

                        var propsInClass =
                            classInNamespace.ChildElements.Where(e => e.ElementType == ElementType.Property).ToList();
                        if (propsInClass.All(p => p.Name != "Id"))
                        {
                            AddViolation(classInNamespace, "EntityClassShouldHaveIdPropertyRule");
                        }

                        if (propsInClass.All(p => p.Name != "Name"))
                        {
                            AddViolation(classInNamespace, "EntityClassShouldHaveNamePropertyRule");
                        }

                        foreach (var prop in propsInClass)
                        {
                            if (prop.Name == "Id" && prop.AccessModifier != AccessModifierType.Public)
                            {
                                AddViolation(prop, "IdPropertyMustBePublicRule");
                            }

                            if (prop.Name == "Name" && prop.AccessModifier != AccessModifierType.Public)
                            {
                                AddViolation(prop, "NamePropertyMustBePublicRule");
                            }
                        }

                        if (!classInNamespace.Attributes.Select(a => a.GetType())
                            .Contains(typeof(DataContractAttribute)))
                        {
                            AddViolation(classInNamespace, "EntityClassShouldHaveDataContractAttributeRule");
                        }
                    }
                }
            }

            return true;
        }
    }
}