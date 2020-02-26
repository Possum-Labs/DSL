using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Characteristics
{
    [TestClass()]
    public class CharacteristicsTests
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void EmptyHit()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics();
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics();
            target[d].Should().Be("bubbles");
        }

        [TestMethod]
        public void SingleHit()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("a");
            target[d].Should().Be("bubbles");
        }

        [TestMethod]
        public void DoubleHit()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a", "b");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("a", "b");
            target[d].Should().Be("bubbles");
        }

        [TestMethod]
        public void DoubleHitOrderMissMatch()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a", "b");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("b", "a");
            target[d].Should().Be("bubbles");
        }

        [TestMethod]
        public void HashcodeOrgerAgnostic()
        {
            var c = new Core.Variables.Characteristics("a", "b");
            var d = new Core.Variables.Characteristics("b", "a");
            c.GetHashCode().Should().Be(d.GetHashCode());
        }

        [TestMethod]
        public void Miss()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("b");
            target.ContainsKey(d).Should().BeFalse();
        }

        [TestMethod]
        public void MissKeySmall()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a", "b", "c");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("a", "b");
            target.ContainsKey(d).Should().BeFalse();
        }

        [TestMethod]
        public void MissKeyBig()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a", "b");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("a", "b", "c");
            target.ContainsKey(d).Should().BeFalse();
        }

        [TestMethod]
        public void MissEmptyKey()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics("a");
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics();
            target.ContainsKey(d).Should().BeFalse();
        }

        [TestMethod]
        public void MissEmptyEntry()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            var c = new Core.Variables.Characteristics();
            target.Add(c, "bubbles");
            var d = new Core.Variables.Characteristics("a");
            target.ContainsKey(d).Should().BeFalse();
        }

        [TestMethod]
        public void ImplicitConversions()
        {
            Dictionary<Core.Variables.Characteristics, string> target = new Dictionary<Core.Variables.Characteristics, string>();
            Core.Variables.Characteristics c = "a, b";
            target.Add(c, "bubbles");
            Core.Variables.Characteristics d = "b, a";
            target[d].Should().Be("bubbles");
        }
    }
}
