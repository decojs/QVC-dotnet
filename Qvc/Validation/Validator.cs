using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Qvc.Executables;

namespace Qvc.Validation
{
    public static class Validator
    {
        public static void Validate(IExecutable executable)
        {
            var context = new ValidationContext(executable, null, null);
            var results = new List<ValidationResult>();

            var valid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(executable, context, results, true);

            if (!valid)
            {
                throw new ValidationException(results.Select(r => new Violation(r.MemberNames.FirstOrDefault(), r.ErrorMessage)).ToList());
            }
        }
    }
}