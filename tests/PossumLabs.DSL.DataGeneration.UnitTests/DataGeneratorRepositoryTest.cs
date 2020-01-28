using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.DataGeneration.UnitTests
{
    [TestClass]
    public class DataGeneratorRepositoryTest
    {
        [TestInitialize]
        public void Setup()
        {
            var objectFactory = new Core.Variables.ObjectFactory();
            var itnerperter = new Core.Variables.Interpeter(objectFactory);
            Target = new DataGeneratorRepository(itnerperter, objectFactory);
        }

        public DataGeneratorRepository Target { get; set; }

        [TestMethod]
        public void Creatures()
        {
            var result = Target.BuildGenerator();
            result.Creatures.GetValue().Should().NotBeEmpty();
        }

        [TestMethod]
        public void FemaleFirstNames()
        {
            var result = Target.BuildGenerator();
            result.FemaleFirstNames.GetValue().Should().NotBeEmpty();
        }

        [TestMethod]
        public void LastNames()
        {
            var result = Target.BuildGenerator();
            result.LastNames.GetValue().Should().NotBeEmpty();
        }

        [TestMethod]
        public void MaleFirstNames()
        {
            var result = Target.BuildGenerator();
            result.MaleFirstNames.GetValue().Should().NotBeEmpty();
        }

        [TestMethod]
        public void Seeds()
        {
            var result = Target.BuildGenerator();
            result.Seeds.GetValue().Should().NotBeEmpty();
        }
    }
}
