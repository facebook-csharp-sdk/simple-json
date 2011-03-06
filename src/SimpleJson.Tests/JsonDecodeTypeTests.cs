namespace SimpleJsonTests
{
    using System;
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

    public class JsonDecodeTypeTests
    {
        private class Person
        {
            public string FirstName { get; set; }

            private string LastName { get; set; }

            public AddressInfo Address { get; set; }

            private string[] Langauges;

            private IEnumerable<string> Hobby;
        }

        private class AddressInfo
        {
            public string Country { get; set; }

            private string City;
        }

        [TestMethod]
        public void NullStringTests()
        {
            var result = SimpleJson.JsonDecode(null, typeof(Person));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NullStringGenericTests()
        {
            var result = SimpleJson.JsonDecode<Person>(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void BooleanTrueTests()
        {
            var json = "true";
            var result = SimpleJson.JsonDecode<bool>(json);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BooleanFalseTests()
        {
            var json = "false";
            var result = SimpleJson.JsonDecode<bool>(json);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NullTests()
        {
            var json = "null";
            var result = SimpleJson.JsonDecode<object>(json);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringTests()
        {
            var json = "\"hello world\"";
            var result = SimpleJson.JsonDecode<string>(json);

            Assert.AreEqual("hello world", result);
        }

        [TestMethod]
        public void methodname()
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

            var json = SimpleJson.JsonEncode(obj);
            var result = SimpleJson.JsonDecode<Person>(json);
        }
    }
}