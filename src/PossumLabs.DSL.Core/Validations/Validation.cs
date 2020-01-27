using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Validations
{
    public class Validation
    {
        public Validation(Func<object, string> predicate, string text)
        {
            Predicate = predicate;
            Text = text;
        }

        public Func<object, string> Predicate { get; }
        public string Text { get; }

        public virtual Exception Validate(object o)
        {
            var msg = Predicate.Invoke(o);
            if (!string.IsNullOrWhiteSpace(msg))
                return new ValidationException(msg);
            return null;
        }

        public void Invoke(object o)
        {
            var msg = Predicate.Invoke(o);
            if (!string.IsNullOrWhiteSpace(msg))
                throw new ValidationException(msg);
        }
    }
}
