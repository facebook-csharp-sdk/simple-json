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

namespace SimpleJsonTests.DataContractTests
{
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
    public class PublicGetterSetterDeserializeTests
    {
        [TestMethod]
        public void DeserializesCorrectly()
        {
            var json = "{\"DataMemberWithoutName\":\"1\",\"name\":\"2\"}";
            var obj = new DataContractPublicGetterSetters();

            var result = (DataContractPublicGetterSetters)SimpleJson.DeserializeObject(json, typeof(DataContractPublicGetterSetters), SimpleJson.DataContractJsonSerializerStrategy);

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.DataMemberWithoutName);
            Assert.AreEqual("2", result.DatMemberWithName);
            Assert.AreEqual(obj.IgnoreDataMember, result.IgnoreDataMember);
            Assert.AreEqual(obj.NoDataMember, result.NoDataMember);
        }
    }
}