using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PossumLabs.DSL.Core.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<ValueMemberInfo> GetValueMembers(this Type t)
            => t.CachedGetFields()
                .Select(f => new ValueMemberInfo(f))
                .Concat(t.CachedGetProperties()
                .Select(p => new ValueMemberInfo(p)));

        public static ValueMemberInfo GetValueMember(this Type t, string name, StringComparison comparison = StringComparison.InvariantCulture)
            => t.CachedGetFields()
                .Select(f => new ValueMemberInfo(f))
                .Concat(t.CachedGetProperties()
                .Select(p => new ValueMemberInfo(p)))
            .Where(m=>m.Name.Equals(name, comparison))
            .FirstOrDefault();

        public static bool HasAllPropertiesAndFieldsOf(this object target, object subset)
        {
            if (target == null && subset == null)
                return true;

            if (target == null || subset == null)
                return false;

            var targetType = target.GetType();
            var subsetType = subset.GetType();

            if (!targetType.IsInstanceOfType(subset))
                return false;

            if (target is IComparable && subset is IComparable)
                return 0 == ((IComparable)target).CompareTo(subset);
            
            if (target is IEnumerable  && subset is IEnumerable)
            {
                var targetValues = ((IEnumerable)target).AsQueryable().Cast<object>();
                var subsetValues = ((IEnumerable)subset).AsQueryable().Cast<object>();
                var missing = subsetValues.Except(targetValues, new EqualityComparer<object>((s, t) => t.HasAllPropertiesAndFieldsOf(s)));
                return missing.None();
            }

            var targetMembers = targetType.GetValueMembers();
            var subsetMembers = subsetType.GetValueMembers();
            foreach (var member in subsetMembers.Where(m => m.HasValue(subset)))
            {
                var targetMember = targetMembers.FirstOrDefault(m => m.Name == member.Name);
                if (targetMember == null || !targetMember.GetValue(target).HasAllPropertiesAndFieldsOf(member.GetValue(subset)))
                    return false;
            }

            return true;
        }

        public static T MapTo<T>(this Dictionary<string, KeyValuePair<string, string>> values, Interpeter interpeter, ObjectFactory f)
            => (T)values.MapTo(typeof(T), interpeter, f);

        public static object MapTo(this Dictionary<string, KeyValuePair<string, string>> values, Type desiredType, Interpeter interpeter, ObjectFactory f)
        {
            var members = desiredType.GetValueMembers();
            var constructors = desiredType.CachedGetConstructors();

            var groups = values.Keys.Select(x => Split(x).First());
            var unmatched = groups.Except(members.Select(x => x.Name.ToUpper()));
            var errors = unmatched.Except(constructors.SelectMany(c => c.GetParameters().Select(p => p.Name.ToUpper())));

            if (errors.Any())
            {
                var prefix = values[errors.First()].Key.Substring(0, values[errors.First()].Key.Length - errors.First().Length);
                var unused = members.Select(p => p.Name).Where(p => !groups.Contains(p.ToUpper()));
                throw new GherkinException($"The columns:{errors.LogFormat(x => values[x].Key)} are unmatched maybe it is one of these {unused.LogFormat(x => prefix + x)}");
            }

            object resolve(string name, Type t)
            {
                var keys = values.Keys.Where(k => k.Split().First() == name);
                object item;
                if (keys.One() && keys.First().Split().One())
                    item = interpeter.Get(t, values[name].Value);
                else if (keys.Any(k => k.Split().One()))
                    throw new GherkinException($"You can't specify the object and set properties on the object, columns {keys.LogFormat(x => values[x].Key)} are in conflict");
                else
                    item = keys.ToDictionary(x => x.Recurse(), x => values[x]).MapTo(t, interpeter, f);

                foreach (var k in keys.ToList())
                    values.Remove(k);

                return item;
            };

            object ret;
            if (unmatched.Any())
            {
                var possibles = constructors.Where(c => unmatched.Except(c.GetParameters().Select(p => p.Name.ToUpper())).None());
                var valid = possibles.Where(c => c.GetParameters().Where(p => !p.IsOptional).Select(p => p.Name.ToUpper()).Except(groups).None());
                var ctor = valid.OrderBy(c => c.GetParameters().Count()).Reverse().FirstOrDefault();

                if (ctor == null)
                    throw new GherkinException($"the following fields '{unmatched.LogFormat()}' did not map to any constructor or property.");

                ret = ctor.Invoke(ctor
                    .GetParameters()
                    .Select(p => groups.Contains(p.Name.ToUpper())?resolve(p.Name.ToUpper(), p.ParameterType): p.DefaultValue)
                    .ToArray());
            }
            else
                ret = f.CreateInstance(desiredType);

            foreach (var key in values.Keys.Select(k => Split(k).First()).ToList())
            {
                var prop = members.First(p => p.Name.ToUpper() == key);
                prop.SetValue(ret, resolve(key, prop.Type));
            }

            return ret;
        }

        public static IEnumerable<string> Split(this string name) 
            => name.Split(new[] { '.', ' ' });

        public static string Recurse(this string name) 
            => name.Split().Skip(1).Aggregate((x, y) => x + " " + y);

        private static IEnumerable<string> Splitter(string name) => name.Split(new[] { '.', ' ' });

        public static object Resolve<T>(this T source, string name)
        {
            object item = source;
            var path = Splitter(name);
            foreach (var piece in path)
            {
                if (item == null)
                    throw new ValidationException($"the member '{piece}' of '{name}' does not exist on null");
                var members = item.GetType().GetValueMembers();
                var member = members.FirstOrDefault(p => p.Name.ToUpper() == piece.ToUpper());
                if (member == null)
                    throw new GherkinException($"The path '{name}' cannot be resolved, the piece '{piece}' does not match any of these options {members.LogFormat(p => p.Name)}");
                item = member.GetValue(item);
            }
            return item;
        }

        public static T ToEnum<T>(this string name) where T : struct
        {
            if (!Enum.TryParse<T>(name, out T e))
                throw new GherkinException($"Unable to conver {name} to Enumeration {typeof(T).Name} please use one of these {Enum.GetNames(typeof(T)).LogFormat()}");
            return e;
        }
    }
}
