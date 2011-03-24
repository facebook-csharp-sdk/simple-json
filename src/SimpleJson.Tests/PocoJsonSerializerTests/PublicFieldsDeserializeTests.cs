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

    public class PublicFieldsDeserializeTests
    {
        [TestMethod]
        public void DeserializesNullObjectCorrectly()
        {
            var json = "null";

            var result = (DataContractPublicFields)SimpleJson.DeserializeObject(json, typeof(DataContractPublicFields), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeserializesEmptyObjectCorrectly()
        {
            var json = "{}";
            var obj = new DataContractPublicFields();

            var result = (DataContractPublicFields)SimpleJson.DeserializeObject(json, typeof(DataContractPublicFields), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNotNull(result);

            Assert.AreEqual(obj.NoDataMember, result.NoDataMember);
            Assert.AreEqual(obj.IgnoreDataMember, result.IgnoreDataMember);
            Assert.AreEqual(obj.DatMemberWithName, result.DatMemberWithName);
            Assert.AreEqual(obj.DataMemberWithoutName, result.DataMemberWithoutName);
        }

        [TestMethod]
        public void DeserializesCorrectly()
        {
            var json = "{\"DataMemberWithoutName\":\"1\",\"DatMemberWithName\":\"2\",\"IgnoreDataMember\":\"3\",\"NoDataMember\":\"4\"}";

            var result = (DataContractPublicFields)SimpleJson.DeserializeObject(json, typeof(DataContractPublicFields), SimpleJson.PocoJsonSerializerStrategy);

            Assert.IsNotNull(result);

            Assert.AreEqual("1", result.DataMemberWithoutName);
            Assert.AreEqual("2", result.DatMemberWithName);
            Assert.AreEqual("3", result.IgnoreDataMember);
            Assert.AreEqual("4", result.NoDataMember);
        }
    }
}