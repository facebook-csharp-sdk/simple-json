namespace SimpleJsonTests.PocoJsonSerializerTests
{
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
    using SimpleJsonTests.DataContractTests;

    public class PrivateFieldsDeserializeTests
    {
        [TestMethod]
        public void DeserializesNullCorrectly()
        {
            var json = "null";

            var result = SimpleJson.DeserializeObject(json, typeof(DataContractPrivateFields), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeserializesEmptyObjectCorrectly()
        {
            var json = "{}";

            var result = SimpleJson.DeserializeObject(json, typeof(DataContractPrivateFields), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNotNull(result);
        }
    }
}