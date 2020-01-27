using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Logging
{
    [TestClass]
    public class ImageLoggingTest
    {
        [TestMethod]
        public void Initialize()
        {
            var target = new ImageLogging(new PossumLabs.DSL.Core.Configuration.ImageLoggingConfig());
        }
    }
}
