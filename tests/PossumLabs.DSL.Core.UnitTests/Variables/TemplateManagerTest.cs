using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    [TestClass]
    public class TemplateManagerTest
    {
        private Interpeter Interpeter { get; set; }
        private ObjectFactory ObjectFactory { get; set; }
        private ExistingDataManager ExistingDataManager { get; set; }
        private TemplateManager TemplateManager { get; set; }
        private RepositoryBase<VariablesTestChildEntity> VariablesTestChildEntityRepository { get; set; }
        private RepositoryBase<VariablesTestEntity> VariablesTestEntityRepository { get; set; }
        private RepositoryBase<VariablesTestValueObject> VariablesTestValueObjectRepository { get; set; }

        public class Helper : IValueObject
        {
            public int AInt { get; set; }
            public string AString { get; set; }
            public int BInt { get; set; }
            public string BString { get; set; }
        }

        [TestInitialize]
        public void Setup()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
            TemplateManager = new TemplateManager();
            ExistingDataManager = new ExistingDataManager(Interpeter, TemplateManager);

            VariablesTestChildEntityRepository = new RepositoryBase<VariablesTestChildEntity>(Interpeter, ObjectFactory, TemplateManager);
            VariablesTestEntityRepository = new RepositoryBase<VariablesTestEntity>(Interpeter, ObjectFactory, TemplateManager);
            VariablesTestValueObjectRepository = new RepositoryBase<VariablesTestValueObject>(Interpeter, ObjectFactory, TemplateManager);

            VariablesTestChildEntityRepository.InitializeDefault(() => new VariablesTestChildEntity { Name = "from state1" }, "state1");
            VariablesTestChildEntityRepository.InitializeDefault(() => new VariablesTestChildEntity { Name = "from state2" }, "state2");
        }

        [TestMethod]
        public void TestNullCoalesceWithDefault()
        {
        
        }

        [TestMethod]
        public void TestDefaultToRepositoryDefault()
        {

        }

        [TestMethod]
        public void MakeSureSameDefaultForBothAttributes()
        {

        }

        [TestMethod]
        public void TestOption1Template()
        {

        }

        [TestMethod]
        public void TestSecondOption1Template()
        {

        }

        [TestMethod]
        public void TestState1Characteristic()
        {

        }

        [TestMethod]
        public void TestSecondState1Characteristic()
        {

        }

        [TestMethod]
        public void TestStateAndCharacteristic()
        {

        }
    }
}
