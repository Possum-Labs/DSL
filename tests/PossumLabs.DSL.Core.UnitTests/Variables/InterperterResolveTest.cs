using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    [TestClass]
    public class InterperterUnitTest
    {
        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }
        private TestTypeRepository Repository { get; }

        public InterperterUnitTest()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
            Repository = new TestTypeRepository( Interpeter, ObjectFactory);
            Interpeter.Register(Repository);
        }

        public class TestType : IValueObject {
            public string a { get; set; }
            public TestType[] array { get; set; }
            public List<TestType> list { get; set; }
        }

        public class ParrentType : TestType
        {
            new public string a { get; set; }
        }

        public class TestTypeRepository : RepositoryBase<TestType>
        {
            public TestTypeRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
            {
            }
        }

        public class ParrentTypeRepository : RepositoryBase<ParrentType>
        {
            public ParrentTypeRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
            {
            }
        }

        [TestMethod]
        public void Simple()
        {
            Repository.Add("k1", new TestType { a = "test" });
            Interpeter.Get<TestType>("k1").a
                .Should().Be("test");
        }

        [TestMethod]
        public void SimplePath()
        {
            Repository.Add("k1", new TestType { a = "test" });
            Interpeter.Get<string>("k1.a")
                .Should().Be("test");
        }

        [TestMethod]
        public void SimpleDownCast()
        {
            Repository.Add("k1", new TestType { a = "test" });
            ((TestType)Interpeter.Get<object>("k1")).a
                .Should().Be("test");
        }

        [TestMethod]
        public void OneLevelDownCast()
        {
            var r = new ParrentTypeRepository(Interpeter, ObjectFactory);
            Interpeter.Register(r);
            r.Add("k1", new ParrentType { a = "test" });
            ((ParrentType)Interpeter.Get<TestType>("k1")).a
                .Should().Be("test");
        }

        //TODO:Bas
        [TestMethod]
        public void SimpleIndexedArray()
        {
            Repository.Add("k1", new TestType { a = "test", array = new[] { new TestType { a = "kid" } } });
            Interpeter.Get<string>("k1.array[0].a")
                .Should().Be("kid");
        }

        [TestMethod]
        public void SimpleIndexedList()
        {
            Repository.Add("k1", new TestType { a = "test", list = new List<TestType> { new TestType { a = "kid" } } });
            Interpeter.Get<string>("k1.list[0].a")
                .Should().Be("kid");
        }

        [TestMethod]
        public void ExceptionIndexedArrayNull()
        {
            Repository.Add("k1", new TestType { a = "test" });
            Interpeter.Invoking(x => x.Get<string>("k1.array[0].a"))
                .Should().Throw<GherkinException>().WithMessage("Unable to resolve [0] of k1.array[0].a");
        }

        [TestMethod]
        public void ExceptionIndexedListNull()
        {
            Repository.Add("k1", new TestType { a = "test" });
            Interpeter.Invoking(x => x.Get<string>("k1.list[0].a"))
                .Should().Throw<GherkinException>().WithMessage("Unable to resolve [0] of k1.list[0].a");
        }

        [TestMethod]
        public void ExceptionIndexedArrayOutOfRange()
        {
            Repository.Add("k1", new TestType { a = "test", array = new[] { new TestType { a = "kid" } } });
            Interpeter.Invoking(x=>x.Get<string>("k1.array[1].a"))
                .Should().Throw<GherkinException>().WithMessage("Index [1] of k1.array[1].a is out of range, there are not enough elements");
        }

        [TestMethod]
        public void ExceptionIndexedListOutOfRange()
        {
            Repository.Add("k1", new TestType { a = "test", list = new List<TestType> { new TestType { a = "kid" } } });
            Interpeter.Invoking(x => x.Get<string>("k1.list[1].a"))
                .Should().Throw<GherkinException>().WithMessage("Argument [1] of k1.list[1].a is out of range, there are not enough elements");
        }
    }
}
