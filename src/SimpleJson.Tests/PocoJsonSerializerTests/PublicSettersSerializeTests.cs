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
#endif

    using SimpleJson;
    using SimpleJsonTests.DataContractTests;

    public class PublicSettersSerializeTests
    {
        private DataContractPublicSetters _dataContractPublicSetters;

        public PublicSettersSerializeTests()
        {
            _dataContractPublicSetters = new DataContractPublicSetters();
        }

        [Test]
        public void SerializesCorrectly()
        {
            var result = SimpleJson.SerializeObject(_dataContractPublicSetters,
                                                    SimpleJson.PocoJsonSerializerStrategy);

            Assert.AreEqual("{}", result);
        }
    }
}