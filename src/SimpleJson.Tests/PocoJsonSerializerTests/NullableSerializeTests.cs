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

    [TestClass]
    public class NullableSerializeTests
    {
        [TestMethod]
        public void SerializeNullableTypeThatIsNotNull()
        {
            var obj = new NullableTypeClass();
            obj.Value = null;

            var json = SimpleJson.SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"Value\":null}", json);
        }

        [TestMethod]
        public void SerializeNullableTypeThatIsNull()
        {
            var obj = new NullableTypeClass();
            obj.Value = 4;

            var json = SimpleJson.SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"Value\":4}", json);
        }

        public class NullableTypeClass
        {
            public int? Value { get; set; }
        }
    }
}
