namespace SimpleJsonTests.PocoDeserializerTests
{
    using System;
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
    public class ArrayTests
    {
        [TestMethod]
        public void StronglyTypeArrayTests()
        {
            string json = "{\"Y\":[\"a\",\"b\"]}";

            var result = SimpleJson.SimpleJson.DeserializeObject<X>(json);

#if SIMPLE_JSON_WINRT
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Y);
#else
            Assert.NotNull(result);
            Assert.NotNull(result.Y);
#endif

            Assert.AreEqual(2, result.Y.Length);
            Assert.AreEqual("a", result.Y[0]);
            Assert.AreEqual("b", result.Y[1]);
        }

        class X
        {
            public string[] Y { get; set; }

            public override string ToString()
            {
                return SimpleJson.SimpleJson.SerializeObject(this);
            }
        }
    }
}