using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using PossumLabs.DSL.Core.FluidDataCreation;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    [TestClass]
    public class SetupUnitTestJson
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
            Driver = new SetupDriver<Setup>(Setup, interperter);
        }

        private Setup Setup { get; set; }
        private SetupDriver<Setup> Driver { get; set; }

        /// <summary>
        /// These are tests that are all the basics
        /// </summary>
        
        [TestMethod]
        public void CreateAParentObject()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1""}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjects()
        {
            Driver.Processor(@"{""ParentObjects"":[{""count"":2}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [TestMethod]
        public void CreateAParentObjectWithACustomName()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""name"":""Bob""}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Name.Should().Be("Bob");
        }

        [TestMethod]
        public void CreateAParentObjectWithACustomNameBeforeChildObject()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""name"":""Bob"", ""ChildObjects"":[{""var"":""C1""}]}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Name.Should().Be("Bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjectsWithACustomNameBeforeChildObjects()
        {
            Driver.Processor(@"{""ParentObjects"":[{""count"":2, ""name"":""Bob"", ""ChildObjects"":[{""count"":1}]}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Name.Should().Be("Bob");

            DataCreatorFactory.ParentObjectDataCreator.Store[1].Id.Should().Be(2);
            DataCreatorFactory.ParentObjectDataCreator.Store[1].Name.Should().Be("Bob");

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(2);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[1].Id.Should().Be(2);
        }

        [TestMethod]
        public void CreateAParentObjectWithChildObject()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1""}]}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ParentObjectDataCreator.Store[0].Id.Should().Be(1);

            DataCreatorFactory.ChildObjectDataCreator.Store.Should().HaveCount(1);
            DataCreatorFactory.ChildObjectDataCreator.Store[0].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateAParentObjectWithChildObjectAccessingData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1""}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateChildObjectTemplatesListsData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""template"":""ListString""}, {""var"":""C2"", ""template"":""ListInt""}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateListStringNoData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""ListOfStrings"":[]}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        [TestMethod]
        public void CreateListStringData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""ListOfStrings"":[""Bob""]}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }


        [TestMethod]
        public void CreateListStringsData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""ListOfStrings"":[""Bob"",""Mary""]}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        public void CreateListIntNoData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""ListOfInts"":[]}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }

        public void CreateListIntData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""ListOfInts"":[42]}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);
        }


        [TestMethod]
        public void CreateListIntsData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1"", ""ChildObjects"":[{""var"":""C1"", ""ListOfInts"":[1,2]}]}]}");

            Setup.ParentObjects["P1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].Id.Should().Be(1);

            Setup.ChildObjects["C1"].ListOfInts.Should().Contain(1);
            Setup.ChildObjects["C1"].ListOfInts.Should().Contain(2);
        }

        [TestMethod]
        public void CreateAParentObjectsWithACustomNameAndChildObjectsAccessingData()
        {
            Driver.Processor(@"{""ParentObjects"":[{""count"":2, ""name"":""Bob"", ""ChildObjects"":[{""count"":1}]}]}");

            Setup.ParentObjects["1"].Id.Should().Be(1);
            Setup.ParentObjects["1"].Name.Should().Be("Bob");

            Setup.ParentObjects["2"].Id.Should().Be(2);
            Setup.ParentObjects["2"].Name.Should().Be("Bob");

            Setup.ChildObjects["1"].Id.Should().Be(1);
            Setup.ChildObjects["2"].Id.Should().Be(2);
        }

        /// <summary>
        /// Templates
        /// </summary>

        [TestMethod]
        public void CreateAParentObjectWithTemplates()
        {
            Driver.Processor(@"{""ParentObjects"":[{""var"":""P1""},{""var"":""P2"", ""template"":""option1""},{""var"":""P3"", ""template"":""option2""}]}");

            Setup.ParentObjects["P1"].Value.Should().Be(55);
            Setup.ParentObjects["P2"].Value.Should().Be(1);
            Setup.ParentObjects["P3"].Value.Should().Be(2);
        }

        /// <summary>
        /// Object Factory
        /// </summary>

        [TestMethod]
        public void CreateLinks()
        {
            Driver.Processor(@"
{""ParentObjects"":[{
    ""var"":""P1"", 
    ""name"":""Bob"", 
    ""ChildObjects"":[{""var"":""C1""},{""var"":""C2""}], 
    ""relations"":[{""first"":""C1"", ""second"":""C2""}]
}]}");

            DataCreatorFactory.ParentObjectDataCreator.Store[0].Links.Keys.Count.Should().Be(1);

        }

    }
}
