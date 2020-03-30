using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IOCContainer.Attributes;
using IOCContainer.Exceptions;
using IOCContainer.Models;
using IOCContainer.Services;

namespace IOCContainer
{
    public class Container
    {
        private readonly List<RegistrationType> _registeredTypes;
        private readonly ValidationService _validationService;

        public Container()
        {
            _validationService = new ValidationService();
            _registeredTypes = new List<RegistrationType>();
        }

        public void AddAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => t.IsClass))
            {
                if (type.GetCustomAttribute<ImportConstructorAttribute>() != null)
                {
                    RegisterTypeAsSelf(type);
                    continue;
                }

                var hasImportAttribute =
                    type.GetProperties()
                        .Any(pi => pi.GetCustomAttribute<ImportAttribute>() != null);

                if (hasImportAttribute)
                {
                    RegisterTypeAsSelf(type);
                    continue;
                }

                var exportAttribute = type.GetCustomAttribute<ExportAttribute>();
                if (exportAttribute != null)
                {
                    RegisterType(type, exportAttribute.Contract ?? type);
                }
            }

            var validationResults = _validationService.ValidateDependencies(_registeredTypes.ToList());
            foreach (var validationResult in validationResults)
            {
                if (!validationResult.ValidationPassed)
                {
                    throw new DependenciesNotFoundException(validationResult);
                }
            }
        }

        public void RegisterTypeAsSelf(Type type) => RegisterType(type, type);

        public void RegisterType(Type type, Type baseType)
        {
            if (_validationService.ValidateType(type, baseType))
            {
                if (_registeredTypes.All(rt => rt.Type != type))
                {
                    var registerType = new RegistrationType(type, baseType);
                    _validationService.SetRegisterTypeKind(registerType);
                    _registeredTypes.Add(registerType);
                }
            }
        }

        public object CreateInstance(Type type)
        {
            if (!_validationService.IsTypeRegistered(_registeredTypes, type))
            {
                throw new TypeNotRegisteredException(type);
            }

            var registeredType = _registeredTypes.SingleOrDefault(rt => rt.BaseType == type);
            if (registeredType == null)
            {
                return null;
            }

            if (registeredType.TypeKind == TypeKind.Export)
            {
                return Activator.CreateInstance(registeredType.Type);
            }

            var validationResult = _validationService.ValidateDependencies(_registeredTypes, registeredType);
            if (!validationResult.ValidationPassed)
            {
                throw new DependenciesNotFoundException(validationResult);
            }

            if (registeredType.TypeKind == TypeKind.ImportViaConstructor)
            {
                var dependentInstances = GetDependentInstances(registeredType);
                var constructor = registeredType.Type.GetConstructors().Last();
                return constructor.Invoke(dependentInstances.ToArray());
            }

            if (registeredType.TypeKind == TypeKind.ImportViaProperty)
            {
                var dependentInstances = GetDependentInstances(registeredType).ToList();
                var resultInstance = Activator.CreateInstance(registeredType.Type);
                var properties = resultInstance.GetType().GetProperties()
                    .Where(p => p.GetCustomAttribute<ImportAttribute>() != null).ToList();
                for (var i = 0; i < properties.Count; i++)
                {
                    properties[i].SetValue(resultInstance, dependentInstances[i]);
                }

                return resultInstance;
            }

            return null;
        }

        private IEnumerable<object> GetDependentInstances(RegistrationType registeredType)
        {
            var dependentTypes = new List<RegistrationType>();
            foreach (var dependentType in registeredType.DependentTypes)
            {
                var type = _registeredTypes.FirstOrDefault(rt => rt.BaseType == dependentType);
                if (type != null)
                {
                    dependentTypes.Add(type);
                }
            }

            var dependentInstances = new List<object>();
            foreach (var dependentType in dependentTypes)
            {
                var newInstance = Activator.CreateInstance(dependentType.Type);
                dependentInstances.Add(newInstance);
            }

            return dependentInstances;
        }

        public T CreateInstance<T>() => (T) CreateInstance(typeof(T));
    }
}