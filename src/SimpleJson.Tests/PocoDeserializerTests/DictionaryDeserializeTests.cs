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

namespace SimpleJsonTests.PocoDeserializerTests
{
    using System.Collections.Generic;

#if NUNIT
    using TestMethod = NUnit.Framework.TestAttribute;
    using NUnit.Framework;
#else
#if NETFX_CORE
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
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
            Assert.AreEqual(1L, result["key1"]);
            Assert.AreEqual(5L, result["key2"]);
        }

        [TestMethod]
        public void DeserializeToDictionaryStringLong()
        {
            string json = @"{""key1"":1,""key2"":5}";

            var result = SimpleJson.DeserializeObject<Dictionary<string, long>>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1L, result["key1"]);
            Assert.AreEqual(5L, result["key2"]);
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