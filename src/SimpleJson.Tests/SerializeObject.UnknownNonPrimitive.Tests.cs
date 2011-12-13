namespace SimpleJsonTests
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

    using SimpleJson;

    [TestClass]
    public class SerializeObject_UnknownNonPrimitive_Tests
    {
        private class Dog
        {
            public string Name { get; set; }
        }

        public class ObjProp
        {
            public string PropTypeKnown { get; set; }
            public object PropTypeUnknown { get; set; }
        }

        [TestMethod]
        public void CanSerializeClassInstanceExample()
        {
            const string expected = @"{""Name"":""spot""}";
            var dog = new Dog { Name = "spot" };

            var json = SimpleJson.SerializeObject(dog);

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeAnonymousObject()
        {
            const string expected = @"{""name"":""spot""}";
            var instance = new { name = "spot" };

            var json = SimpleJson.SerializeObject(instance);

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeAnonymousObjectWithNumbers()
        {
            // todo: make json encode smaller by removing spaces after ,
            const string expected = @"{""quantity"":8902,""cost"":45.33,""value"":-0.01063}";
            var instance = new
            {
                quantity = 8902,
                cost = 45.33,
                value = -1.063E-02
            };

            var json = SimpleJson.SerializeObject(instance);
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeWithAnonymousTypes()
        {
            const string expected = @"{""array"":{""quantity"":8902,""cost"":45.33,""value"":-0.01063}}";
            var instance = new
            {
                array = new { quantity = 8902, cost = 45.33, value = -1.063E-02 }
            };

            var json = SimpleJson.SerializeObject(instance);
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeWithDates()
        {
            var instance = new
            {
                now = DateTime.UtcNow
            };

            var json = SimpleJson.SerializeObject(instance);
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void CanSerializeUnknownProperty()
        {
            var instance = new ObjProp { PropTypeKnown = "str", PropTypeUnknown = new { unknown = "property" } };

            var json = SimpleJson.SerializeObject(instance);
            Assert.AreEqual("{\"PropTypeKnown\":\"str\",\"PropTypeUnknown\":{\"unknown\":\"property\"}}", json);
        }
    }
}