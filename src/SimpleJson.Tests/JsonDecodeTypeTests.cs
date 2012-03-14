//-----------------------------------------------------------------------
// <copyright file="<file>.cs" company="The Outercurve Foundation">
//    Copyright (c) 2011, The Outercurve Foundation.
//
//    Licensed under the MIT License (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.opensource.org/licenses/mit-license.php
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// <author>Nathan Totten (ntotten.com), Jim Zimmerman (jimzimmerman.com) and Prabir Shrestha (prabir.me)</author>
// <website>https://github.com/facebook-csharp-sdk/simple-json</website>
//-----------------------------------------------------------------------

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

    [TestClass]
    public class DeserializeObjectTypeTests
    {
// Disable never used warnings for fields - they are actually used in deserialization
#pragma warning disable 0169, 0649
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
#pragma warning restore 0169, 0649

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
        public void DecimalToIntTest()
        {
            var json = "10.2";
            var result = SimpleJson.DeserializeObject<int>(json);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void GivenNumberWithoutDecimalTypeIsLong()
        {
            var json = "10";
            var result = SimpleJson.DeserializeObject(json);
#if NETFX_CORE
            Assert.IsInstanceOfType(result, typeof(long));
#else
            Assert.IsInstanceOf<long>(result);
#endif
        }

        [TestMethod]
        public void GivenNumberWithDecimalTypeIsDouble()
        {
            var json = "10.2";
            var result = SimpleJson.DeserializeObject(json);

#if NETFX_CORE
            Assert.IsInstanceOfType(result, typeof(double));
#else
          Assert.IsInstanceOf<double>(result);  
#endif
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

        [TestMethod]
        public void ClassArrayTests()
        {
            var json = "{\"Name\":\"person1\",\"HobbyArray\":[{\"name\":\"basketball\",\"value\":10},{\"name\":\"football\",\"value\":9}]}";

            var result = SimpleJson.DeserializeObject<HobbyPersonArray>(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ClassListTests()
        {
            var json = "{\"Name\":\"person1\",\"HobbyArray\":[{\"name\":\"basketball\",\"value\":10},{\"name\":\"football\",\"value\":9}]}";

            var result = SimpleJson.DeserializeObject<HobbyPersonList>(json);

            Assert.IsNotNull(result);
        }

        public class HobbyPersonArray
        {
            public string Name { get; set; }
            public Hobbies[] Hobbies { get; set; }
        }

        public class HobbyPersonList
        {
            public string Name { get; set; }
            public IList<Hobbies> Hobbies { get; set; }
        }

        public class Hobbies
        {
            public string Name;
            public int Value;
        }
    }


}