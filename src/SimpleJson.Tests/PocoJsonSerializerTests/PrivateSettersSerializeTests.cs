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

    public class PrivateSettersSerializeTests
    {
        private DataContractPrivateSetters _dataContractPrivateSetters;

        public PrivateSettersSerializeTests()
        {
            _dataContractPrivateSetters = new DataContractPrivateSetters();
        }

        [Test]
        public void SerializesCorrectly()
        {
            var result = SimpleJson.SerializeObject(_dataContractPrivateSetters,
                                                    SimpleJson.PocoJsonSerializerStrategy);

            Assert.AreEqual("{}", result);
        }
    }
}