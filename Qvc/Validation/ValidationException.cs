using System;
using System.Collections.Generic;
using System.Linq;

namespace Qvc.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
        {
            Violations = new List<Violation>
            {
                new Violation(string.Empty, message)
            };
        }

        public ValidationException(string fieldName, string message)
        {
            Violations = new List<Violation>
            {
                new Violation(fieldName, message)
            };
        }

        public ValidationException(IReadOnlyCollection<Violation> violations)
        {
            Violations = violations.ToList();
        }

        public List<Violation> Violations { get; set; }
    }
}