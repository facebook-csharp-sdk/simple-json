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

    public class PrivateGetterSettersDeserializeTests
    {
        [TestMethod]
        public void SerializesNullCorrectly()
        {
            var json = "null";

            var result = SimpleJson.DeserializeObject(json, typeof(DataContractPrivateGetterSetters), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SerializesEmptyObjectCorrectly()
        {
            var json = "{}";

            var result = SimpleJson.DeserializeObject(json, typeof(DataContractPrivateGetterSetters), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNotNull(result);
        }
    }
}