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

    public class ToStringTests
    {
        [Test]
        public void ToStringCallingSerializeObjectOnItself()
        {
            var x = new X { Y = "z" };

            var json = SimpleJson.SimpleJson.SerializeObject(x);

            Assert.AreEqual("{\"Y\":\"z\"}", json);
        }

        class X
        {
            public string Y { get; set; }

            public override string ToString()
            {
                return SimpleJson.SimpleJson.SerializeObject(this, SimpleJson.SimpleJson.PocoJsonSerializerStrategy);
            }
        }
    }
}