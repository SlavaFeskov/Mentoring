using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyIoC.Attributes;
using MyIoC.Models;
using MyIoC.Sample;
using MyIoC.Services;
using MyIoC.Utils;

namespace MyIoC
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
                if (type.HasCustomAttribute<ImportConstructorAttribute>())
                {
                    RegisterTypeAsSelf(type);
                    continue;
                }

                var hasImportAttribute =
                    type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance)
                        .Any(pi => pi.HasCustomAttribute<ImportAttribute>());

                if (hasImportAttribute)
                {
                    RegisterTypeAsSelf(type);
                    continue;
                }

                if (type.HasCustomAttribute<ExportAttribute>())
                {
                    var exportAttribute = type.GetCustomAttribute<ExportAttribute>(true);
                    var typ = typeof(CustomerDal);
                    var isDal = type.FullName == typ.FullName;
                    var eq = typ == type;
                    var t = typeof(CustomerDal).GetCustomAttribute<ExportAttribute>();
                    //RegisterType(type, exportAttribute.Contract ?? type);
                }
            }

            _validationService.ValidateDependencies(_registeredTypes.ToList());
        }

        public void RegisterTypeAsSelf(Type type) => RegisterType(type, type);

        public void RegisterType(Type type, Type baseType)
        {
            if (_validationService.ValidateType(type, baseType))
            {
                if (_registeredTypes.All(rt => rt.Type != type))
                {
                    var registerType = new RegistrationType(type, baseType);
                    _validationService.UpdateRegistrationType(registerType);
                    _registeredTypes.Add(registerType);
                }
            }
        }

        public object CreateInstance(Type type)
        {
            var registeredType = _registeredTypes.SingleOrDefault(rt => rt.Type == type);
            if (registeredType == null)
            {
                return null;
            }

            if (registeredType.TypeKind == TypeKind.Export)
            {
                var newInstance = Activator.CreateInstance(registeredType.Type);
                return Convert.ChangeType(newInstance, registeredType.BaseType);
            }

            if (!_validationService.ValidateDependencies(registeredType))
            {
                return null; // throw
            }

            if (registeredType.TypeKind == TypeKind.ImportViaConstructor)
            {
                var dependentInstances = GetDependentInstances(registeredType);
                var resultInstance = Activator.CreateInstance(registeredType.Type, dependentInstances);
                return Convert.ChangeType(resultInstance, registeredType.BaseType);
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

                return Convert.ChangeType(resultInstance, registeredType.BaseType);
            }

            return null;
        }

        private IEnumerable<object> GetDependentInstances(RegistrationType registeredType)
        {
            var dependentTypes =
                registeredType.DependentTypes.Select(dt => _registeredTypes.Single(rt => rt.Type == dt));
            var dependentInstances = new List<object>();
            foreach (var dependentType in dependentTypes)
            {
                var newInstance = Activator.CreateInstance(dependentType.Type);
                var correctTypeInstance = Convert.ChangeType(newInstance, dependentType.BaseType);
                dependentInstances.Add(correctTypeInstance);
            }

            return dependentInstances;
        }

        public T CreateInstance<T>()
        {
            return default;
        }
    }
}