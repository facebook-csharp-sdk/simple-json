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

namespace SimpleJson.Tests.PocoJsonSerializerTests
{
#if NUNIT
    using TestClass = NUnit.Framework.TestFixtureAttribute;
    using TestMethod = NUnit.Framework.TestAttribute;
    using TestCleanup = NUnit.Framework.TearDownAttribute;
    using TestInitialize = NUnit.Framework.SetUpAttribute;
    using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
    using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
    using NUnit.Framework;
    using System;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

    [TestClass]
    public class DateTimeSerializeTests
    {
        [TestMethod]
        public void SerializeDateTime()
        {
            var obj = new SerializeDateTimeTypeClass
                          {
                              Value = new DateTime(2004, 1, 20, 5, 3, 6, DateTimeKind.Utc)
                          };

            var json = SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"Value\":\"2004-01-20T05:03:06Z\"}", json);
        }

        public class SerializeDateTimeTypeClass
        {
            public DateTime Value { get; set; }
        }
    }
}
