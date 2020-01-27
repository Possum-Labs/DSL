using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    [TestClass]
    public class ReflectionExtensionsTest
    {

        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }

        public ReflectionExtensionsTest()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
        }

        public class TestType
        {
            public string a { get; set; }
            public int b { get; set; }
            public int? c { get; set; }
        }

        [TestMethod]
        public void MapToString()
        {
            var values = new Dictionary<string, KeyValuePair<string, string>>();
            values.Add("A", new KeyValuePair<string, string>("a","text"));

            var t = values.MapTo<TestType>(Interpeter, ObjectFactory);

            t.a.Should().Be("text");
        }

        [TestMethod]
        public void MapToNumber()
        {
            var values = new Dictionary<string, KeyValuePair<string, string>>();
            values.Add("B", new KeyValuePair<string, string>("b", "42"));

            var t = values.MapTo<TestType>(Interpeter, ObjectFactory);
            
            t.b.Should().Be(42);
        }

        [TestMethod]
        public void MapToNullableNumber()
        {
            var values = new Dictionary<string, KeyValuePair<string, string>>();
            values.Add("C", new KeyValuePair<string, string>("c", "42"));

            var t = values.MapTo<TestType>(Interpeter, ObjectFactory);

            t.c.Should().Be(42);
        }

    }
}
