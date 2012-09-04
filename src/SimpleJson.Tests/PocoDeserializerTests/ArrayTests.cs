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
    public class ArrayTests
    {
        [TestMethod]
        public void StronglyTypeArrayTests()
        {
            string json = "{\"Y\":[\"a\",\"b\"]}";

            var result = SimpleJson.SimpleJson.DeserializeObject<X>(json);

#if NETFX_CORE
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Y);
#else
            Assert.NotNull(result);
            Assert.NotNull(result.Y);
#endif

            Assert.AreEqual(2, result.Y.Length);
            Assert.AreEqual("a", result.Y[0]);
            Assert.AreEqual("b", result.Y[1]);
        }

        class X
        {
            public string[] Y { get; set; }

            public override string ToString()
            {
                return SimpleJson.SimpleJson.SerializeObject(this);
            }
        }
    }
}