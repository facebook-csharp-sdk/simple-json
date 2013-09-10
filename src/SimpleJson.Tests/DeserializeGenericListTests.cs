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
    public class DeserializeGenericListTests
    {
        private class NumberList : List<int>
        {
        }

        private class ItemList : List<Item>
        {
        }

        private class Item
        {
            public string SomeProperty { get; set; }
        }

        [TestMethod]
        public void Can_Deserialize_Root_Json_Array_Of_Primitives_To_Inherited_List()
        {
            var json = "[0,1,2]";
            var result = SimpleJson.DeserializeObject<NumberList>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }

        [TestMethod]
        public void Can_Deserialize_Root_Json_Array_Of_Primitives_To_Generic_List()
        {
            var json = "[0,1,2]";
            var result = SimpleJson.DeserializeObject<List<int>>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }

        [TestMethod]
        public void Can_Deserialize_Root_Json_Array_To_Inherited_List()
        {
            var json = @"[{""SomeProperty"":""bar0""},{""SomeProperty"":""bar1""}]";
            var result = SimpleJson.DeserializeObject<ItemList>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("bar0", result[0].SomeProperty);
            Assert.AreEqual("bar1", result[1].SomeProperty);
        }
    }
}