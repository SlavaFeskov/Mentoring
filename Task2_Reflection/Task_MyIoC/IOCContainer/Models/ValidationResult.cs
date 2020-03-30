using System;
using System.Collections.Generic;
using System.Linq;

namespace IOCContainer.Models
{
    public class ValidationResult
    {
        public List<Type> RequiredTypes { get; set; }

        public List<Type> FoundTypes { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public bool ValidationPassed => RequiredTypes.All(t => FoundTypes.Contains(t));

        public ValidationResult(RegistrationType registrationType)
        {
            RequiredTypes = new List<Type>();
            FoundTypes = new List<Type>();
            RegistrationType = registrationType;
        }
    }
}