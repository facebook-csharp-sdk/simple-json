
namespace SimpleJsonTests.PocoDeserializerTests
{
    using System.Collections.Generic;

#if NUNIT
    using TestMethod = NUnit.Framework.TestAttribute;
    using NUnit.Framework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

    using SimpleJson;

    public class DictionaryDeserializeTests
    {
        [TestMethod]
        public void DeserializeToIDictionaryStringString()
        {
            string json = @"{""key1"":""value1"",""key2"":""value2""}";

            var result = SimpleJson.DeserializeObject<IDictionary<string, string>>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("value1", result["key1"]);
            Assert.AreEqual("value2", result["key2"]);
        }

        [TestMethod]
        public void DeserializeToDictionaryStringString()
        {
            string json = @"{""key1"":""value1"",""key2"":""value2""}";

            var result = SimpleJson.DeserializeObject<Dictionary<string, string>>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("value1", result["key1"]);
            Assert.AreEqual("value2", result["key2"]);
        }

        [TestMethod]
        public void DeserializeToIDictionaryStringLong()
        {
            string json = @"{""key1"":1,""key2"":5}";

            var result = SimpleJson.DeserializeObject<IDictionary<string, long>>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1l, result["key1"]);
            Assert.AreEqual(5l, result["key2"]);
        }

        [TestMethod]
        public void DeserializeToDictionaryStringLong()
        {
            string json = @"{""key1"":1,""key2"":5}";

            var result = SimpleJson.DeserializeObject<Dictionary<string, long>>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1l, result["key1"]);
            Assert.AreEqual(5l, result["key2"]);
        }

        [TestMethod]
        public void NestedDeserializeDictionary()
        {
            string json = "{\"regions\": [{\"id\": \"US\",\"value\": {\"1\": \"Alabama\"}}]}";

            var result = SimpleJson.DeserializeObject<autocomplete_data>(json);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.regions);
            Assert.AreEqual(1, result.regions.Count);

            var us = result.regions[0];
            Assert.AreEqual("US", us.id);

            var value = us.value;
            Assert.AreEqual(1, value.Count);

            Assert.IsTrue(value.ContainsKey("1"));
            Assert.AreEqual("Alabama", value["1"]);
        }

        class autocomplete_data
        {
            public List<region> regions { get; set; }
        }

        private class region
        {
            public string id { get; set; }

            public IDictionary<string, string> value { get; set; }
        }
    }
}