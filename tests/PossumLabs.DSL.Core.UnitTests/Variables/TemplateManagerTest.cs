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
        }

        [TestMethod]
        public void NoTemplate()
        {

        }
    }
}
