using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using PossumLabs.DSL.Core.Variables;

namespace PossumLabs.DSL.Core.Validations
{
    public class ValidationFactory
    {
        public ValidationFactory(Interpeter interpeter)
        {
            Interpeter = interpeter;
        }

        public Validation Create(string constructor, string field = null)
            => new Validation(o =>
            {
                if (field != null)
                    o = o.Resolve(field);
                if (MakePredicate(constructor).Invoke(o) != true)
                    return $" the value was '{o}' wich was not '{constructor}'";
                return null;
             }, constructor);

        private Interpeter Interpeter { get; }

        public virtual Predicate<object> MakePredicate(string predicate)
        {
            if (Parser.IsNull.IsMatch(predicate))
                return ProcessNull(predicate);
            else if (Parser.IsTest.IsMatch(predicate))
                return ProcessTest(predicate);
            else if (Parser.IsRegex.IsMatch(predicate))
                return ProcessRegex(predicate);
            else if (Parser.IsLitteral.IsMatch(predicate))
                return ProcessLitteral(predicate);
            else if (Parser.IsSubstituted.IsMatch(predicate))
                return ProcessSubstitution(predicate);
            else if (Parser.IsNumber.IsMatch(predicate))
                return ProcessNumber(predicate);
            else if (Parser.IsPercentage.IsMatch(predicate))
                return ProcessPercentage(predicate);
            else if (Parser.IsMoney.IsMatch(predicate))
                return ProcessMoney(predicate);
            else if (Parser.IsJson.IsMatch(predicate))
                return ProcessJson(predicate);
            else
                return v => 
                Interpeter.Convert<string>(v) == Interpeter.Get<string>(predicate);
        }

        public Predicate<object> ProcessSubstitution(string predicate)
        {
            var match = Parser.IsSubstituted.Match(predicate).Groups[1].Value;
            foreach (var token in Parser.FindLitterals.Matches(predicate).Cast<Match>().Select(x => x.Groups[1].Value))
                match = match.Replace("{" + token + "}", Interpeter.Get<string>(token));
            return v => Interpeter.Convert<string>(v) == match;
        }

        public Predicate<object> ProcessMoney(string predicate)
        {
            var number = decimal.Parse(Parser.IsMoney.Match(predicate).Groups[1].Value);
            if (predicate.Contains("(") || predicate.Contains("-"))
                number *= -1;
            return v => Interpeter.Convert<decimal>(v) == number;
        }

        public Predicate<object> ProcessPercentage(string predicate)
        {
            var number = decimal.Parse(Parser.IsPercentage.Match(predicate).Groups[1].Value);
            return v => Interpeter.Convert<decimal>(v) == (number / 100);
        }

        public Predicate<object> ProcessNumber(string predicate)
            => v => Interpeter.Convert<decimal>(v) == decimal.Parse(predicate);

        public Predicate<object> ProcessNull(string predicate)
            => v => v == null;

        public Predicate<object> ProcessLitteral(string predicate)
            => v => Parser.IsLitteral.Match(predicate).Groups[1].Value == Interpeter.Convert<string>(v);

        public Predicate<object> ProcessRegex(string predicate)
            => v => new Regex(Parser.IsRegex.Match(predicate).Groups[1].Value).IsMatch(Interpeter.Convert<string>(v));

        public Predicate<object> ProcessJson(string predicate)
            => v => v.HasAllPropertiesAndFieldsOf(predicate.FromJsonAsTypeOf(v));

        public Predicate<object> ProcessTest(string predicate)
        {
            var comparer = Parser.IsTest.Match(predicate).Groups[1].Value;
            var number = Parser.IsTest.Match(predicate).Groups[2].Value;
            var target = Decimal.Parse(number);
            var low = (!number.Contains(".")) ?
                (target - new decimal(.5)) :
                (target - new decimal(5 * Math.Pow(10, (number.LastIndexOf('.') - number.Length))));
            var high = (!number.Contains(".")) ?
                (target + new decimal(.5)) :
                (target + new decimal(5 * Math.Pow(10, (number.LastIndexOf('.') - number.Length))));
            switch (comparer)
            {
                case "==":
                    return v => Interpeter.Convert<decimal>(v) == target;
                case "~=":
                    return v => Interpeter.Convert<decimal>(v) >= low && Interpeter.Convert<decimal>(v) < high;
                case "!=":
                    return v => Interpeter.Convert<decimal>(v) != target;
                case ">=":
                    return v => Interpeter.Convert<decimal>(v) >= target;
                case ">":
                    return v => Interpeter.Convert<decimal>(v) > target;
                case "<=":
                    return v => Interpeter.Convert<decimal>(v) <= target;
                case "<":
                    return v => Interpeter.Convert<decimal>(v) < target;
                default:
                    throw new GherkinException($"the comparer '{comparer}' is unknown, only ==, ~=, !=, >=, >, <, <= are supported");
            }
        }
    }
}
