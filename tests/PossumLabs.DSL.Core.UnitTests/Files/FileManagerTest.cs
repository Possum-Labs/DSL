using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Files
{
    [TestClass]
    public class FileManagerTest
    {
        [TestInitialize]
        public void BeforeTests()
        {
            var datetimeManager = new DatetimeManager(()=> DateTime.Now);
            Target = new FileManager(datetimeManager);
            Target.Initialize("feature", "scenario");
        }
        public FileManager Target { get; set; }
        [TestMethod]
        public void PersistFile()
        {
            var uri = Target.PersistFile("bob", "hellow world");
            uri.Should().NotBeNull();
        }
    }
}
