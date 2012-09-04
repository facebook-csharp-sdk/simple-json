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

namespace SimpleJsonTests.PocoJsonSerializerTests
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
#if NETFX_CORE
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
#endif

    [TestClass]
    public class NullableSerializeTests
    {
        [TestMethod]
        public void Test()
        {
            DateTime? obj = null;

            var json = SimpleJson.SimpleJson.SerializeObject(obj);

            Assert.AreEqual("null", json);
        }

        [TestMethod]
        public void TestWithValue()
        {
            DateTime? obj = new DateTime(2004, 1, 20, 5, 3, 6, 12, DateTimeKind.Utc);

            var json = SimpleJson.SimpleJson.SerializeObject(obj);

            Assert.AreEqual("\"2004-01-20T05:03:06.012Z\"", json);
        }

        [TestMethod]
        public void SerializeNullableTypeThatIsNotNull()
        {
            var obj = new NullableTypeClass();
            obj.Value = null;

            var json = SimpleJson.SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"Value\":null}", json);
        }

        [TestMethod]
        public void SerializeNullableTypeThatIsNull()
        {
            var obj = new NullableTypeClass();
            obj.Value = 4;

            var json = SimpleJson.SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"Value\":4}", json);
        }

        public class NullableTypeClass
        {
            public int? Value { get; set; }
        }
    }
}
