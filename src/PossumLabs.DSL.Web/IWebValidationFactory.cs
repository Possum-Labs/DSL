using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Web
{
    public interface IWebValidationFactory
    {
        Predicate<object> BuildPredicate(string predicate, Func<Element, bool> test);
        TableValidation Create(List<Dictionary<string, WebValidation>> validation);
        WebValidation Create(string constructor, string field = null);
        Predicate<object> MakePredicate(string predicate);
    }
}