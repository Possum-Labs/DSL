using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTest
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
        public void CreateAParentObject()
        {
            Setup.WithParentObject("P1");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjects()
        {
            Setup.WithParentObjects(2);

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [TestMethod]
        public void CreateAParentObjectWithACustomName()
        {
            Setup.WithParentObject("P1", configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
            });

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Name.Should().Contain("bob");
        }

        [TestMethod]
        public void CreateAParentObjectWithACustomNameBeforeChildObject()
        {
            Setup.WithParentObject("P1", configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
                p.WithChild("C1");
            });

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Name.Should().Contain("bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjectsWithACustomNameBeforeChildObjects()
        {
            Setup.WithParentObjects(2, configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
                p.WithChilderen(1);
            });

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Name.Should().Contain("bob");

            DataCreatorFactory.ParentObjectDataCreator.Store[1].Id.Should().Be(2);
            DataCreatorFactory.ParentObjectDataCreator.Store[1].Name.Should().Contain("bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void CreateAParentObjectWithACustomNameAfterChildObject()
        {
            Setup.WithParentObject("P1", configurer: p =>
            {
                p.WithChild("C1");
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
            });
        }

        [TestMethod]
        public void CreateAParentObjectWithChildObject()
        {
            Setup.WithParentObject("P1", configurer: p =>
            {
                p.WithChild("C1");
            });

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjectWithChildObjectAccessingData()
        {
            Setup.WithParentObject("P1", configurer: p =>
            {
                p.WithChild("C1");
            });

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjectsWithACustomNameBeforeChildObjectsAccessingData()
        {
            Setup.WithParentObjects(2, configurer: p =>
            {
                p.Name = Guid.NewGuid().ToString().Replace("-", "bob");
                p.WithChilderen(1);
            });

            Setup.ParentObjects["1"].Id.Should().Be(1);
            Setup.ParentObjects["1"].Name.Should().Contain("bob");

            Setup.ParentObjects["2"].Name.Should().Contain("bob");
            
            Setup.ChildObjects["1"].Id.Should().Be(1);
            Setup.ChildObjects["2"].Id.Should().Be(2);
        }
    }
}
