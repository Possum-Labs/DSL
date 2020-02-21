using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using PossumLabs.DSL.Core.Variables;

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
            var myEntityRepository = new RepositoryBase<MyEntity>(interpeter, factory);
            interpeter.Register(myEntityRepository);
            var myValueRepository = new RepositoryBase<MyValueObject>(interpeter, factory);
            interpeter.Register(myValueRepository);
            new PossumLabs.DSL.Core.Variables.ExistingDataManager(interpeter, templateManager).Initialize(Assembly.GetExecutingAssembly());
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
