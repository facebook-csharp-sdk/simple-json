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

namespace SimpleJson.Tests.PocoDeserializerTests
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
    public class EnumDeserializeTests
    {
        [TestMethod]
        public void DeserializeEnumStringValue()
        {
            var json = "{\"Value\":\"Two\"}";

            var result = SimpleJson.DeserializeObject<X>(json);
            Assert.AreEqual(Values.Two, result.Value);
        }

        public class X
        {
            public Values Value { get; set; }
        }
    }


    [TestClass]
    public class NullableEnumDeserializeTests
    {
        [TestMethod]
        public void DeserializeNullEnumValue()
        {
            var json = "{}";

            var result = SimpleJson.DeserializeObject<X>(json);

            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void DeserializeNullValue()
        {
            var json = "{\"Value\":null}";

            var result = SimpleJson.DeserializeObject<X>(json);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void DeserializeNullableEnumWithValue()
        {
            var json = "{\"Value\":\"Two\"}";
            var result = SimpleJson.DeserializeObject<X>(json);
            Assert.AreEqual(Values.Two, result.Value);
        }

        public class X
        {
            public Values? Value { get; set; }
        }
    }

    public enum Values
    {
        One,
        Two,
        Three
    }

}
