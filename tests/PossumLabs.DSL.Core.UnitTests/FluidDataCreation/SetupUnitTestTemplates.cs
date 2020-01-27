using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestTemplates
    {
        public DataCreatorFactory DataCreatorFactory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            DataCreatorFactory = new DataCreatorFactory();
            var factory = new PossumLabs.DSL.Core.Variables.ObjectFactory();
            var interperter = new PossumLabs.DSL.Core.Variables.Interpeter(factory);
            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());

            Setup = new Setup(DataCreatorFactory, factory, templateManager, interperter);
        }

        private Setup Setup { get; set; }

        [TestMethod]
        public void CreateAParentObjectWithTemplates()
        {
            Setup.WithParentObject("P1");
            Setup.WithParentObject("P2", "option1");
            Setup.WithParentObject("P3", "option2");

            Setup.ParentObjects["P1"].Value.Should().Be(55);
            Setup.ParentObjects["P2"].Value.Should().Be(1);
            Setup.ParentObjects["P3"].Value.Should().Be(2);
        }

        [TestMethod]
        public void CreateAChildObjectWithTemplatesInts()
        {
            Setup.WithChildObject("C4", "ListInt");

            Setup.ChildObjects["C4"].ListOfInts.Should().Contain(0);
            Setup.ChildObjects["C4"].ListOfInts.Should().Contain(2);
            Setup.ChildObjects["C4"].ListOfInts.Should().Contain(3);
        }

        [TestMethod]
        public void CreateAChildObjectWithTemplatesStrings()
        {
            Setup.WithChildObject("C3", "ListString");

            Setup.ChildObjects["C3"].ListOfStrings.Should().Contain("Bob");
            Setup.ChildObjects["C3"].ListOfStrings.Should().Contain("Mary");
        }
    }
}
