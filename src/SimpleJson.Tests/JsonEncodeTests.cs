namespace SimpleJsonTests
{
    using System;
    using System.Collections.Generic;

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
    public class JsonEncodeTests
    {
        [TestMethod]
        public void GuidEncode()
        {
            Guid guid = new Guid("BED7F4EA-1A96-11d2-8F08-00A0C9A6186D");
            var json = SimpleJson.JsonEncode(guid);

            Assert.AreEqual(@"""bed7f4ea-1a96-11d2-8f08-00a0c9a6186d""", json);
        }

        [TestMethod]
        public void SerializeEnum()
        {
            string json = SimpleJson.JsonEncode(StringComparison.CurrentCultureIgnoreCase);
            Assert.AreEqual("1", json);
        }
    }
}