namespace SimpleJsonTests.DataContractTests
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
                                                    SimpleJson.DataContractJsonSerializerStrategy);

            Assert.AreEqual("{}", result);
        }
    }
}