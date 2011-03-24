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

    public class PublicFieldsSerializeTests
    {
        private DataContractPublicFields _dataContractPublicFields;

        public PublicFieldsSerializeTests()
        {
            _dataContractPublicFields = new DataContractPublicFields();
        }

        [Test]
        public void SerializesCorrectly()
        {
            var result = SimpleJson.SerializeObject(_dataContractPublicFields,
                                                    SimpleJson.PocoJsonSerializerStrategy);

            Assert.AreEqual("{\"DataMemberWithoutName\":\"dmv\",\"DatMemberWithName\":\"dmnv\",\"IgnoreDataMember\":\"idm\",\"NoDataMember\":\"ndm\"}", result);
        }
    }
}