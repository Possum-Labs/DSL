using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestExisting
    {
        public DataCreatorFactory DataCreatorFactory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            DataCreatorFactory = new DataCreatorFactory();
            var factory = new PossumLabs.DSL.Core.Variables.ObjectFactory();
            var interpeter = new PossumLabs.DSL.Core.Variables.Interpeter(factory);
            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Setup = new Setup(DataCreatorFactory, factory, templateManager, interpeter);

            new PossumLabs.DSL.Core.Variables.ExistingDataManager(interpeter).Initialize(Assembly.GetExecutingAssembly());
        }

        private Setup Setup { get; set; }


        [TestMethod]
        public void MakeSureExistingDataIsLoaded()
        {
            Setup.ParentObjects["OG"].Value.Should().Be(42);
            Setup.ParentObjects["OG"].Category.Should().Be("the OG");

            DataCreatorFactory.ParentObjectDataCreator.Store.Count.Should().Be(0);
        }
    }
}
