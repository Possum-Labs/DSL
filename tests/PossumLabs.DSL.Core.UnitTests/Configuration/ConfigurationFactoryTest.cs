using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Configuration
{
    [TestClass]
    public class ConfigurationFactoryTest
    {
        //TODO: add non-json tests, non-attribute property tests, non-attribute class test , field tests
        [TestInitialize]
        public void Init()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = configBuilder.Build();

            Target = new ConfigurationFactory(config);
        }

        public ConfigurationFactory Target { get; set; }

        [ConfigurationObject("Ab")]
        public class AbConfig
        {
            [ConfigurationMember("UU")]
            public string UU { get; set; }
            [ConfigurationMember("UL")]
            public string ul { get; set; }
            [ConfigurationMember("lu")]
            public string LU { get; set; }
            [ConfigurationMember("ll")]
            public string ll { get; set; }
            [ConfigurationMember("Int")]
            public int Int { get; set; }
            [ConfigurationMember("Double")]
            public double Double { get; set; }
        }

        [TestMethod]
        public void UU()
        {
            var r = Target.Create<AbConfig>();
            r.UU.Should().Be("Bob");
        }

        [TestMethod]
        public void Ul()
        {
            var r = Target.Create<AbConfig>();
            r.ul.Should().Be("Bob");
        }

        [TestMethod]
        public void Lu()
        {
            var r = Target.Create<AbConfig>();
            r.LU.Should().Be("Bob");
        }

        [TestMethod]
        public void ll()
        {
            var r = Target.Create<AbConfig>();
            r.ll.Should().Be("Bob");
        }

        [TestMethod]
        public void Double()
        {
            var r = Target.Create<AbConfig>();
            r.Double.Should().Be(42);
        }

        [TestMethod]
        public void Int()
        {
            var r = Target.Create<AbConfig>();
            r.Int.Should().Be(42);
        }
    }
}
