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

    public class PublicGetterSettersSerializeTests
    {
        private DataContractPublicGetterSetters _dataContractPublicGetterSetters;

        public PublicGetterSettersSerializeTests()
        {
            _dataContractPublicGetterSetters = new DataContractPublicGetterSetters();
        }

        [Test]
        public void SerializesCorrectly()
        {
            var result = SimpleJson.SerializeObject(_dataContractPublicGetterSetters,
                                                    SimpleJson.DataContractJsonSerializerStrategy);

            Assert.AreEqual("{\"DataMemberWithoutName\":\"dmv\",\"name\":\"dmnv\"}", result);
        }
    }
}