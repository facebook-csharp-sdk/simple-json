
namespace SimpleJson.Tests.PocoDeserializerTests
{
#if NUNIT
    using System;
    using System.Globalization;
    using TestClass = NUnit.Framework.TestFixtureAttribute;
    using TestMethod = NUnit.Framework.TestAttribute;
    using TestCleanup = NUnit.Framework.TearDownAttribute;
    using TestInitialize = NUnit.Framework.SetUpAttribute;
    using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
    using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
    using NUnit.Framework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

    [TestClass]
    public class DateTimeDeserializeTests
    {
        [TestMethod]
        public void Test()
        {
            var json = "{\"Value\":\"2004-01-20T05:03:06Z\"}";

            var result = SimpleJson.DeserializeObject<SerializeDateTimeTypeClass>(json).Value;
            Assert.AreEqual(2004, result.Year);
            Assert.AreEqual(1, result.Month);
            Assert.AreEqual(20, result.Day);
            Assert.AreEqual(5, result.Hour);
            Assert.AreEqual(3, result.Minute);
            Assert.AreEqual(6, result.Second);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        public class SerializeDateTimeTypeClass
        {
            public DateTime Value { get; set; }
        }
    }
}
