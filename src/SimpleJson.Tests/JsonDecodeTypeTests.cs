namespace SimpleJsonTests
{
    using System.Collections.Generic;
    using System.Linq;

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

    public class DeserializeObjectTypeTests
    {
        private class Person
        {
            public string FirstName { get; set; }

            private string LastName { get; set; }

            public AddressInfo Address { get; set; }

            public string[] Langauges;

            public IEnumerable<string> Hobby;

            private string[] _nothing;

            public string[] Nothing { get { return _nothing; } }
        }

        private class AddressInfo
        {
            public string Country { get; set; }

            private string City;
        }

        [TestMethod]
        public void NullStringTests()
        {
            var result = SimpleJson.DeserializeObject(null, typeof(Person));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NullStringGenericTests()
        {
            var result = SimpleJson.DeserializeObject<Person>(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void BooleanTrueTests()
        {
            var json = "true";
            var result = SimpleJson.DeserializeObject<bool>(json);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BooleanFalseTests()
        {
            var json = "false";
            var result = SimpleJson.DeserializeObject<bool>(json);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NullTests()
        {
            var json = "null";
            var result = SimpleJson.DeserializeObject<object>(json);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringTests()
        {
            var json = "\"hello world\"";
            var result = SimpleJson.DeserializeObject<string>(json);

            Assert.AreEqual("hello world", result);
        }

        [TestMethod]
        public void ArrayAndListDeserializationTests()
        {
            var obj = new
            {
                FirstName = "Prabir",
                LastName = "Shrestha",
                Address = new { Country = "Nepal", City = "Kathmandu" },
                Langauges = new[] { "English", "Nepali" },
                Hobby = new[] { "Guitar", "Swimming", "Basketball" },
                Nothing = new[] { "nothing" }
            };

            var json = SimpleJson.SerializeObject(obj);
            var result = SimpleJson.DeserializeObject<Person>(json);

            Assert.AreEqual(obj.FirstName, result.FirstName);
            Assert.AreEqual(obj.Address.Country, result.Address.Country);

            Assert.AreEqual(obj.Langauges.Length, result.Langauges.Length);
            Assert.AreEqual(obj.Langauges[0], result.Langauges[0]);
            Assert.AreEqual(obj.Langauges[1], result.Langauges[1]);

            var hobies = result.Hobby.ToList();
            Assert.AreEqual(obj.Hobby.Length, hobies.Count);
            Assert.AreEqual(obj.Hobby[0], hobies[0]);
            Assert.AreEqual(obj.Hobby[1], hobies[1]);
            Assert.AreEqual(obj.Hobby[2], hobies[2]);

            Assert.IsNull(result.Nothing);
        }
    }
}