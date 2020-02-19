using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    [TestClass]
    public class ExistingDataManagerTest
    {
        private Interpeter Interpeter { get; set; }
        private ObjectFactory ObjectFactory { get; set; }
        private ExistingDataManager ExistingDataManager { get; set; }
        private TemplateManager TemplateManager { get; set; }

        public class Helper : IValueObject
        {
            public int AInt { get; set; }
            public string AString { get; set; }
            public int BInt { get; set; }
            public string BString { get; set; }
        }

        public class NullingExpandoObject : DynamicObject, IEnumerable
        {
            private readonly Dictionary<string, object> values
                = new Dictionary<string, object>();

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                // We don't care about the return value...
                values.TryGetValue(binder.Name, out result);
                return true;
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                values[binder.Name] = value;
                return true;
            }

            public Field this[string name]  
                => new Field {  Name = name, Value = values[name] };

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public NullingExpandoObjectEnumerator GetEnumerator()
            {
                return new NullingExpandoObjectEnumerator(values);
            }

            public class NullingExpandoObjectEnumerator : IEnumerator
            {
                private Dictionary<string, object> values;

                public NullingExpandoObjectEnumerator(Dictionary<string, object> values)
                {
                    this.values = values;
                }

                int position = -1;

                public bool MoveNext()
                {
                    position++;
                    return (position < values.Keys.Count);
                }

                public void Reset()
                {
                    position = -1;
                }

                object IEnumerator.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                public Field Current
                {
                    get
                    {
                        try
                        {
                            return new Field  { Name = values.Keys.ToArray()[position]};
                        }
                        catch (IndexOutOfRangeException)
                        {
                            throw new InvalidOperationException();
                        }
                    }
                }
            }

            public class Field
            {
                public string Name { get; set; }
                public object Value { get; set; }
            }
        }

        [TestInitialize]
        public void Setup()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
            TemplateManager = new TemplateManager();
            ExistingDataManager = new ExistingDataManager(Interpeter, TemplateManager);
        }

        [TestMethod]
        public void NoTemplate()
        {
            dynamic source = new NullingExpandoObject();
            source.AString = "bubbles";
            source.name = "bob";
            var result = ExistingDataManager.ProcessVariable(typeof(Helper), source);
            var r = result as Helper;
            r.AString.Should().Be("bubbles");
        }

        [TestMethod]
        public void DefaultTemplate()
        {
            TemplateManager.Register<Helper>((x) => { x.BInt = 42; });
            dynamic source = new NullingExpandoObject();
            source.AString = "bubbles";
            source.Name = "bob";
            var result = ExistingDataManager.ProcessVariable(typeof(Helper), source);
            var r = result as Helper;
            r.AString.Should().Be("bubbles");
            r.BInt.Should().Be(42);
        }

        [TestMethod]
        public void NamedTemplate()
        {
            TemplateManager.Register<Helper>((x) => { x.BString = "bobber"; }, "someTemplate");
            dynamic source = new NullingExpandoObject();
            source.AString = "bubbles";
            source.Name = "bob";
            source.template = "someTemplate";
            var result = ExistingDataManager.ProcessVariable(typeof(Helper), source);
            var r = result as Helper;
            r.AString.Should().Be("bubbles");
            r.BString.Should().Be("bobber");
        }

        [TestMethod]
        public void NamedTemplateNotUsed()
        {
            TemplateManager.Register<Helper>((x) => { x.BString = "bobber"; }, "someTemplate");
            dynamic source = new NullingExpandoObject();
            source.AString = "bubbles";
            source.Name = "bob";
            var result = ExistingDataManager.ProcessVariable(typeof(Helper), source);
            var r = result as Helper;
            r.AString.Should().Be("bubbles");
            r.BString.Should().BeNull();
        }

        [TestMethod]
        public void EnvironmentVariableOverride()
        {
            Environment.SetEnvironmentVariable("bob.AString", "Value1");
            try
            {
                dynamic source = new NullingExpandoObject();
                source.AString = "bubbles";
                source.Name = "bob";
                var result = ExistingDataManager.ProcessVariable(typeof(Helper), source);
                var r = result as Helper;
                r.AString.Should().Be("Value1");
            }
            finally
            {
                Environment.SetEnvironmentVariable("bob.AString", null);
            }
        }

        [TestMethod]
        public void EnvironmentVariableOverrideTypeConversion()
        {
            Environment.SetEnvironmentVariable("bob.AInt", "42");
            try
            {
                dynamic source = new NullingExpandoObject();
                source.Name = "bob";
                var result = ExistingDataManager.ProcessVariable(typeof(Helper), source);
                var r = result as Helper;
                r.AInt.Should().Be(42);
            }
            finally
            {
                Environment.SetEnvironmentVariable("bob.AInt", null);
            }
        }
    }
}
