using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Web.Diagnostic;
using System;

namespace PossumLabs.DSL.Web.Test
{
    [TestClass]
    public class NetworkWatcherTest
    {
        [TestInitialize]
        public void Init()
        {
            Target = new NetworkWatcher();
        }

        private NetworkWatcher Target;

        [TestMethod]
        public void AddUrlNoPredicate()
        {
            Target.AddUrl("bob");
            Target.LastGoodUrl.Should().Be("bob");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void ErrorOut()
        {
            try
            {
                Target.ErrorOut("bob");
            }
            finally
            {
                Target.LastGoodUrl.Should().BeNull();
                Target.BadUrl.Should().Be("bob");
            }
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void AddUrlPredicatePasses()
        {
            Target.UrlErrorTester = (x) => true;
            try
            {
                Target.ErrorOut("bob");
            }
            finally
            {
                Target.LastGoodUrl.Should().BeNull();
                Target.BadUrl.Should().Be("bob");
            }
        }

        
        [TestMethod]
        public void AddUrlPredicateFails()
        {
            Target.UrlErrorTester = (x) => false;
            Target.AddUrl("bob");
            Target.LastGoodUrl.Should().Be("bob");
        }
    }
}
