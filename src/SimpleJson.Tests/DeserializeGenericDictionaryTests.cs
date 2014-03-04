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

namespace SimpleJsonTests {
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
#if NETFX_CORE
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
#endif

    using SimpleJson;

    [TestClass]
    public class DeserializeGenericDictionaryTests {
        private class Configuration {
            public Configuration() {
                Settings = new SettingsDictionary();
            }

            public int Version { get; set; }

            public SettingsDictionary Settings { get; set; }
        }

        private class SettingsDictionary : Dictionary<string, string> {
            public SettingsDictionary() : base(StringComparer.OrdinalIgnoreCase) {}

            public SettingsDictionary(IEnumerable<KeyValuePair<string, string>> values) : base(StringComparer.OrdinalIgnoreCase) {
                foreach (var kvp in values)
                    Add(kvp.Key, kvp.Value);
            }
        }

        [TestMethod]
        public void Can_Deserialize_Json_Object_With_Inherited_Typed_Dictionary() {
            const string json = "{\"Version\":9,\"Settings\":{}}";

            var result = SimpleJson.DeserializeObject<Configuration>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual(9, result.Version);
            Assert.IsNotNull(result.Settings);
        }
    }
}