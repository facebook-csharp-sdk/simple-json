namespace SimpleJsonTests.DataContractTests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

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
    public class PublicGetterSetterDeserializeTests
    {
        [TestMethod]
        public void DeserializesCorrectly()
        {
            var json = "{\"DataMemberWithoutName\":\"1\",\"name\":\"2\"}";
            var obj = new DataContractPublicGetterSetters();

            var result = (DataContractPublicGetterSetters)SimpleJson.DeserializeObject(json, typeof(DataContractPublicGetterSetters), SimpleJson.DataContractJsonSerializerStrategy);

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.DataMemberWithoutName);
            Assert.AreEqual("2", result.DatMemberWithName);
            Assert.AreEqual(obj.IgnoreDataMember, result.IgnoreDataMember);
            Assert.AreEqual(obj.NoDataMember, result.NoDataMember);
        }
    }
}