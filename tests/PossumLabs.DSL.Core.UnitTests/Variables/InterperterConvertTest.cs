using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    [TestClass]
    public class InterperterConvertTest
    {
        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }

        public InterperterConvertTest()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
        }

        [TestMethod]
        public void ReturnNullWhenObjectNull()
            =>Interpeter.Convert(typeof(MyDomainObject), null)
                .Should().BeNull("nulls should stay null if possible");

        [TestMethod]
        public void ConvertToNullable()
            => Interpeter.Convert(typeof(int?), Convert.ToInt32(42)).Should().Be(42);

        [TestMethod]
        public void ConvertToNullableIntFromByte()
            => Interpeter.Convert(typeof(int?), Convert.ToByte(42)).Should().Be(42);

        [TestMethod]
        public void ConvertToNullableLongFromByte()
            => Interpeter.Convert(typeof(long?), Convert.ToByte(42)).Should().Be(42);

        [TestMethod]
        public void ConvertToNullableBigger()
        => Interpeter.Convert(typeof(long?), Convert.ToInt32(42)).Should().Be(42);

        [TestMethod]
        public void ConvertToBigger()
            => Interpeter.Convert(typeof(long), Convert.ToInt32(42)).Should().Be(42);

        [TestMethod]
        public void ConvertfromString()
            => Interpeter.Convert(typeof(int), "42").Should().Be(42);

        [TestMethod]
        public void ConvertToNullableFromString()
            => Interpeter.Convert(typeof(int?), "42").Should().Be(42);

        [TestMethod]
        public void ConvertBulk()
        {
            var types = new List<Type> {
                typeof(byte),
                typeof(int),
                typeof(long),
                typeof(Int16),
                typeof(UInt16),
                typeof(Int32),
                typeof(UInt32),
                typeof(Int64),
                typeof(UInt64),
                typeof(byte?),
                typeof(int?),
                typeof(long?),
                typeof(Nullable<Int16>),
                typeof(Nullable<UInt16>),
                typeof(Nullable<Int32>),
                typeof(Nullable<UInt32>),
                typeof(Nullable<Int64>),
                typeof(Nullable<UInt64>)
            };
            foreach (var targetType in types)
            {
                foreach (var sourceType in types)
                {
                    var i = Interpeter.Convert(sourceType, "42");
                    i.Should().Be(42);
                    Interpeter.Convert(targetType, i).Should().Be(42);
                }
            }
        }
    }
}
