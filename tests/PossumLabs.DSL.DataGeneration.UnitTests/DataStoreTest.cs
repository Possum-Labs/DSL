using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.DataGeneration.UnitTests
{
    [TestClass]
    public class DataStoreTest
    {
        [TestInitialize]
        public void Setup()
        {
            Target = new DataStore("bob");
        }

        public DataStore Target { get; set; }

        [TestMethod]
        public void Initialize()
        {
            Target.Initialize("1\n2\n3\n4\n5\n6\n7\n8\n9\n10");
            var ret = Target.GetValue();
            int.Parse(ret).Should().BeInRange(1, 10);
        }

        [TestMethod]
        public void Percentile()
        {
            Target.Initialize("1\n2\n3\n4\n5\n6\n7\n8\n9\n10");
            Target.Percentile = 10;
            var ret = Target.GetValue();
            int.Parse(ret).Should().BeInRange(1, 1);
        }
    }
}
