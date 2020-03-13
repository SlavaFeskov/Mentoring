using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyIoC.Attributes;
using MyIoC.Models;

namespace MyIoC.Services
{
    public class ValidationService
    {
        public bool ValidateType(Type type, Type baseType)
        {
            var validationResult = true;
            var registerAvailable = false;
            var exportAttribute = type.GetCustomAttribute<ExportAttribute>();
            if (exportAttribute != null)
            {
                registerAvailable = true;
                validationResult = exportAttribute.Contract == baseType && type.IsAssignableFrom(baseType);
            }

            var importConstructorAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
            if (importConstructorAttribute != null)
            {
                registerAvailable = true;
                validationResult = type == baseType;
            }

            var hasImportAttribute =
                type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance)
                    .Any(pi => pi.GetCustomAttribute<ImportAttribute>() != null);
            if (hasImportAttribute)
            {
                registerAvailable = true;
                validationResult = type != baseType;
            }

            return validationResult && registerAvailable;
        }

        public bool ValidateDependencies(List<RegistrationType> registrationTypes)
        {
            foreach (var registrationType in registrationTypes.Where(rt =>
                rt.TypeKind == TypeKind.ImportViaConstructor || rt.TypeKind == TypeKind.ImportViaProperty))
            {
                var importType = registrationType;
                foreach (var dependentType in importType.DependentTypes)
                {
                    if (registrationTypes.All(rt => rt.BaseType != dependentType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool ValidateDependencies(RegistrationType registrationType)
        {
            return ValidateDependencies(new List<RegistrationType> {registrationType});
        }

        public RegistrationType UpdateRegistrationType(RegistrationType registrationType)
        {
            var exportAttribute = registrationType.Type.GetCustomAttribute<ExportAttribute>();
            if (exportAttribute != null)
            {
                registrationType.TypeKind = TypeKind.Export;
                return registrationType;
            }

            var importViaConstructorAttribute = registrationType.Type.GetCustomAttribute<ImportConstructorAttribute>();
            if (importViaConstructorAttribute != null)
            {
                var dependentTypes = registrationType.Type.GetMethods().Single(mi => mi.IsConstructor).GetParameters()
                    .Where(p => !p.GetType().IsValueType)
                    .Select(p => p.GetType());
                registrationType.TypeKind = TypeKind.ImportViaConstructor;
                registrationType.DependentTypes = dependentTypes;
                return registrationType;
            }

            var importProperties =
                registrationType.Type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance)
                    .Where(p => p.GetCustomAttribute<ImportAttribute>() != null).ToList();
            if (importProperties.Count != 0)
            {
                registrationType.TypeKind = TypeKind.ImportViaProperty;
                registrationType.DependentTypes = importProperties.Select(p => p.GetGetMethod().ReturnType);
            }

            return registrationType;
        }
    }
}