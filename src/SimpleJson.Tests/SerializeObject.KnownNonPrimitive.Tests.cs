
namespace SimpleJsonTests
{
    using System;
#if NUNIT
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

    using SimpleJson;

    [TestClass]
    public class SerializeObject_KnownNonPrimitive_Tests
    {
        [TestMethod]
        public void GuidSerialization()
        {
            Guid guid = new Guid("BED7F4EA-1A96-11d2-8F08-00A0C9A6186D");
            var json = SimpleJson.SerializeObject(guid);

            Assert.AreEqual(@"""bed7f4ea-1a96-11d2-8f08-00a0c9a6186d""", json);
        }

        [TestMethod]
        public void EnumSerialization()
        {
            string json = SimpleJson.SerializeObject(StringComparison.CurrentCultureIgnoreCase);
            Assert.AreEqual("1", json);
        }

        [TestMethod]
        public void UriSerialization()
        {
            string json = SimpleJson.SerializeObject(new Uri("http://simplejson.codeplex.com/"));
            Assert.AreEqual("\"http://simplejson.codeplex.com/\"", json);
        }
    }
}