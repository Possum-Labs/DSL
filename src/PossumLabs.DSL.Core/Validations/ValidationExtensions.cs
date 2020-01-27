using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.Validations
{
    public static class ValidationExtensions
    {
        public static Exception[] GetFailedValidations(this object o, IEnumerable<Validation> validations)
         => validations
                .Select(x => x.Validate(o))
                .Where(x => x != null)
                .ToArray();

        public static void Validate(this object o, params Validation[] validations)
            => o.Validate(validations.ToList());

        public static void Validate(this object o, IEnumerable<Validation> validations)
        {
            var failedVaidations = o.GetFailedValidations(validations);

            if (failedVaidations.Any())
                throw new AggregateException(failedVaidations.OrderBy(e => e.Message.Length));
        }

        public static bool Contains(this IEnumerable o, Validation validation)
            => o.Cast<object>().Where(x => validation.Validate(x) == null).Any();

        public static bool Contains(this IEnumerable o, IEnumerable<Validation> validations)
            => o.Cast<object>().Select(x => x.GetFailedValidations(validations)).Any(x => x.None());

        public static bool ValidateContains(this IEnumerable o, IEnumerable<IEnumerable<Validation>> validationRows)
        {
            var missing = validationRows.Where(validations => !o.Cast<object>().Contains(validations));

            if (missing.Any())
                throw new ValidationException($"Unable to find {missing.LogFormat(v=>v.Text)}");
            return true;
        }

        public static bool ValidateContains(this object o, IEnumerable<IEnumerable<Validation>> validationRows)
            => o.ConvertToIEnumerable().ValidateContains(validationRows);

        public static bool ValidateContains(this object o, Validation validation)
            => o.ConvertToIEnumerable().Contains(validation);
    }
}
