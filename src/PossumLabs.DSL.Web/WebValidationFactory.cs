using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Validations;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PossumLabs.DSL.Web
{
    public class WebValidationFactory:ValidationFactory
    {
        public WebValidationFactory(Interpeter interpeter) : base(interpeter)
        {
        }

        new public WebValidation Create(string constructor, string field = null)
            => new WebValidation(o =>
            {
                if (field != null)
                    o = o.Resolve(field);
                if (MakePredicate(constructor).Invoke(o) != true)
                    return $"the value was '{((Element)o).Values.Where(s=>!String.IsNullOrWhiteSpace(s)).LogFormat()}' which was not '{constructor}'";
                return null;
            }, constructor);

        public TableValidation Create(List<Dictionary<string,WebValidation>> validation)
            => new TableValidation(validation);

        public override Predicate<object> MakePredicate(string predicate)
        {
            if (Parser.IsElement.IsMatch(predicate))
                return BuildPredicate(predicate,(e)=>e.Tag == Parser.IsElement.Match(predicate).Groups[1].Value);
            if (Parser.IsClass.IsMatch(predicate))
                return BuildPredicate(predicate, (e) => e.Classes.Contains(Parser.IsClass.Match(predicate).Groups[1].Value));
            if (Parser.IsId.IsMatch(predicate))
                return BuildPredicate(predicate, (e) => e.Id == Parser.IsId.Match(predicate).Groups[1].Value);
            return BuildPredicate(predicate, (e) => e.Values.Any(value=> base.MakePredicate(predicate).Invoke(value)));
        }

        public Predicate<object> BuildPredicate(string predicate, Func<Element,bool> test)
        {
            return v =>
            {
                if (v is Element)
                {
                    var e = v as Element;
                    return test(e);
                }
                else
                    throw new GherkinException($"the predicate {predicate} only works on Elements");
            };
        }
    }
}
