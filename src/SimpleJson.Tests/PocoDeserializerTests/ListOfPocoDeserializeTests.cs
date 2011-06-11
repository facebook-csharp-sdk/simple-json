using System.Collections.Generic;

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

    public class ListOfPocoDeserializeTests
    {

        [TestMethod]
        public void CorrectlyDeserializesListOfPoco()
        {
            string json = "{\"colleges\":[{\"id\": 16777217,\"value\":\"Harvard\"},{\"id\": 16777218,\"value\":\"Columbia\"}]}";

            var result = SimpleJson.SimpleJson.DeserializeObject<autocomplete_data>(json);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.colleges);
            Assert.AreEqual(2, result.colleges.Count);

            var harvard = result.colleges[0];
            var columbia = result.colleges[1];

            Assert.IsNotNull(harvard);
            Assert.AreEqual(16777217, harvard.id);
            Assert.AreEqual("Harvard", harvard.value);
            Assert.IsNotNull(columbia);
            Assert.AreEqual(16777218, columbia.id);
            Assert.AreEqual("Columbia", columbia.value);
        }

        class autocomplete_data
        {
            public List<college> colleges { get; set; }
        }

        class college
        {
            public long id { get; set; }

            public string value { get; set; }
        }
    }
}