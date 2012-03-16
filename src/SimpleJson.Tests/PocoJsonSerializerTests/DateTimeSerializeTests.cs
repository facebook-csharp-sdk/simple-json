
namespace SimpleJson.Tests.PocoJsonSerializerTests
{
#if NUNIT
    using TestClass = NUnit.Framework.TestFixtureAttribute;
    using TestMethod = NUnit.Framework.TestAttribute;
    using TestCleanup = NUnit.Framework.TearDownAttribute;
    using TestInitialize = NUnit.Framework.SetUpAttribute;
    using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
    using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
    using NUnit.Framework;
    using System;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

    [TestClass]
    public class DateTimeSerializeTests
    {
        [TestMethod]
        public void SerializeDateTime()
        {
            var obj = new SerializeDateTimeTypeClass
                          {
                              Value = new DateTime(2004, 1, 20, 5, 3, 6, DateTimeKind.Utc)
                          };

            var json = SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"Value\":\"2004-01-20T05:03:06.0000000Z\"}", json);
        }

        public class SerializeDateTimeTypeClass
        {
            public DateTime Value { get; set; }
        }
    }
}
