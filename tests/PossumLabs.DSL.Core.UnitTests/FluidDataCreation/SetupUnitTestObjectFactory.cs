using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestObjectFactory
    {
        public DataCreatorFactory DataCreatorFactory { get; set; }
        public PossumLabs.DSL.Core.Variables.ObjectFactory factory { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            DataCreatorFactory = new DataCreatorFactory();
            factory = new PossumLabs.DSL.Core.Variables.ObjectFactory();
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
            factory.Register<ValueObject>(o => new ValueObject { Name = "special" });

            Setup.WithParentObject("P1", configurer: p=>p.ComplexValue.Value=42);

            Setup.ParentObjects["P1"].ComplexValue.Value.Should().Be(42);
            Setup.ParentObjects["P1"].ComplexValue.Name.Should().Be("special");
            
        }
    }
}
