using System;
using System.Linq;
using MyIoC.Models;

namespace MyIoC.Exceptions
{
    public class DependenciesNotFoundException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public override string Message =>
            $"Dependencies for type {ValidationResult.RegistrationType.Type} not found. The following types are not registered: {string.Join(", ", ValidationResult.RequiredTypes.Except(ValidationResult.FoundTypes))}";

        public DependenciesNotFoundException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}