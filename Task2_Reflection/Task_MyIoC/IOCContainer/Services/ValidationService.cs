using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IOCContainer.Attributes;
using IOCContainer.Models;

namespace IOCContainer.Services
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
                if (exportAttribute.Contract == null)
                {
                    validationResult = baseType.IsAssignableFrom(type);
                }
                else
                {
                    validationResult = exportAttribute.Contract == baseType && baseType.IsAssignableFrom(type);
                }
            }

            var importConstructorAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
            if (importConstructorAttribute != null)
            {
                registerAvailable = true;
                validationResult = type == baseType;
            }

            var hasImportAttribute =
                type.GetProperties()
                    .Any(pi => pi.GetCustomAttribute<ImportAttribute>() != null);
            if (hasImportAttribute)
            {
                registerAvailable = true;
                validationResult = type == baseType;
            }

            return validationResult && registerAvailable;
        }

        public ValidationResult ValidateDependencies(List<RegistrationType> registrationTypes, RegistrationType registeredType)
        {
            var validationResult = new ValidationResult(registeredType);
            foreach (var dependentType in registeredType.DependentTypes)
            {
                validationResult.RequiredTypes.Add(dependentType);
                if (registrationTypes.SingleOrDefault(rt => rt.BaseType == dependentType) != null)
                {
                    validationResult.FoundTypes.Add(dependentType);
                }
            }

            return validationResult;
        }

        public List<ValidationResult> ValidateDependencies(List<RegistrationType> registrationTypes)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var registrationType in registrationTypes.Where(rt =>
                rt.TypeKind == TypeKind.ImportViaConstructor || rt.TypeKind == TypeKind.ImportViaProperty))
            {
                validationResults.Add(ValidateDependencies(registrationTypes, registrationType));
            }

            return validationResults;
        }

        public bool IsTypeRegistered(List<RegistrationType> registrationTypes, Type type)
        {
            var registeredType = registrationTypes.FirstOrDefault(rt => rt.BaseType == type);
            if (registeredType == null)
            {
                return false;
            }

            return true;
        }

        public RegistrationType SetRegisterTypeKind(RegistrationType registrationType)
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
                var constructor = registrationType.Type.GetConstructors().Last();
                var dependentTypes = constructor.GetParameters()
                    .Where(p => !p.GetType().IsValueType)
                    .Select(p => p.ParameterType).ToList();
                registrationType.TypeKind = TypeKind.ImportViaConstructor;
                registrationType.DependentTypes = dependentTypes;
                return registrationType;
            }

            var importProperties =
                registrationType.Type.GetProperties()
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