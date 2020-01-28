using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.DataGeneration.UnitTests
{
    [TestClass]
    public class DataStoresManagerTest
    {
        [TestInitialize]
        public void Setup()
        {
            Target = new DataStoresManager();
        }

        public DataStoresManager Target { get; set; }

        [TestMethod]
        public void LoadEmbeddedResources()
        {
            var ret = Target.LoadEmbeddedResources();
            ret.Should().HaveCount(5);
            ret.Should().Contain(x => x.Name == "creatures");
        }
    }
}
