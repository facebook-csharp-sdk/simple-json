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
    using System.Collections.Generic;

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
    public class SerializeObject_Primitive_Tests
    {
        [TestMethod]
        public void ObjectSerialization()
        {
            object value;

            value = 1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = 1.1;
            Assert.AreEqual("1.1", SimpleJson.SerializeObject(value));

            value = 1.1m;
            Assert.AreEqual("1.1", SimpleJson.SerializeObject(value));

            //value = (float)1.1;
            //Assert.AreEqual("1.1", SimpleJson.JsonEncode(value));

            value = (short)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = (long)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = (byte)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = (uint)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = (ushort)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = (sbyte)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = (ulong)1;
            Assert.AreEqual("1", SimpleJson.SerializeObject(value));

            value = null;
            Assert.AreEqual("null", SimpleJson.SerializeObject(value));

            //value = DBNull.Value;
            //Assert.AreEqual("null", SimpleJson.JsonEncode(value));

            value = "I am a string";
            Assert.AreEqual(@"""I am a string""", SimpleJson.SerializeObject(value));

            value = true;
            Assert.AreEqual("true", SimpleJson.SerializeObject(value));

            value = false;
            Assert.AreEqual("false", SimpleJson.SerializeObject(value));

            //value = 'c';
            //Assert.AreEqual(@"""c""", SimpleJson.JsonEncode(value));
        }

        [TestMethod]
        public void StringSerialization()
        {
            string str = "I am a string";
            Assert.AreEqual(@"""I am a string""", SimpleJson.SerializeObject(str));
        }

        [TestMethod]
        public void StringEscpaingSerialization()
        {
            string v = @"It's a good day
""sunshine""";

            string json = SimpleJson.SerializeObject(v);
            Assert.AreEqual(@"""It's a good day\r\n\""sunshine\""""", json);
        }

        [TestMethod]
#if NETFX_CORE
        [Ignore]
#else
        [Ignore("not part of the json standard.")]
#endif
        public void CharSerialization()
        {
            Assert.AreEqual(@"""c""", SimpleJson.SerializeObject('c'));
        }

        [TestMethod]
        public void BoolTrueSerialization()
        {
            Assert.AreEqual("true", SimpleJson.SerializeObject(true));
        }

        [TestMethod]
        public void BoolFalseSerialization()
        {
            Assert.AreEqual("false", SimpleJson.SerializeObject(false));
        }

        [TestMethod]
        public void NullSerialization()
        {
            Assert.AreEqual("null", SimpleJson.SerializeObject(null));
        }

#if !NETFX_CORE
        [TestMethod]
        [Ignore("uncomment if(Convert.IsDBNull(input)) in PocoJsonSerializerStrategy.TrySerializeKnownTypes. disabled to improve performance.")]
        public void DbNullSerialization()
        {
            Assert.AreEqual("null", SimpleJson.SerializeObject(System.DBNull.Value));
        }
#endif

        [TestMethod]
        public void Int16Serialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((short)1));
        }

        [TestMethod]
        public void UnsingedInt16Serialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((ushort)1));
        }

        [TestMethod]
        public void Int64Serialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((long)1));
        }

        [TestMethod]
        public void UnsingedInt64Serialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((ulong)1));
        }

        [TestMethod]
        public void ByteSerialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((byte)1));
        }

        [TestMethod]
        public void SignedByteSerialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((sbyte)1));
        }

        [TestMethod]
        public void Int32Serialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((int)1));
        }

        [TestMethod]
        public void UnsingedInt32Serialization()
        {
            Assert.AreEqual("1", SimpleJson.SerializeObject((uint)1));
        }

        [TestMethod]
        public void DoubleSerialization()
        {
            Assert.AreEqual("1.1", SimpleJson.SerializeObject(1.1));
            Assert.AreEqual("NaN", SimpleJson.SerializeObject(double.NaN));
            Assert.AreEqual("-Infinity", SimpleJson.SerializeObject(double.NegativeInfinity));
            // Check with a strategy that does other things to Infinity/NaN
            Assert.AreEqual("\"-Infinity\"", SimpleJson.SerializeObject(double.NegativeInfinity, new InfinityAsStringJsonSerializerStrategy()));
            Assert.AreEqual("\"NaN\"", SimpleJson.SerializeObject(double.NaN, new InfinityAsStringJsonSerializerStrategy()));
        }

        [TestMethod]
        public void DecimalSerialization()
        {
            Assert.AreEqual("1.1", SimpleJson.SerializeObject(1.1m));
            Assert.AreEqual("1.11", SimpleJson.SerializeObject(1.11m));
            Assert.AreEqual("1.111", SimpleJson.SerializeObject(1.111m));
            Assert.AreEqual("1.1111", SimpleJson.SerializeObject(1.1111m));
            Assert.AreEqual("1.11111", SimpleJson.SerializeObject(1.11111m));
            Assert.AreEqual("1.111111", SimpleJson.SerializeObject(1.111111m));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1.0m));
            //Assert.AreEqual("-1.0", SimpleJson.JsonEncode(-1.0m));
            //Assert.AreEqual("-1.0", SimpleJson.JsonEncode(-1m));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1m));
            Assert.AreEqual("1.01", SimpleJson.SerializeObject(1.01m));
            Assert.AreEqual("1.001", SimpleJson.SerializeObject(1.001m));
            //Assert.AreEqual("79228162514264337593543950335.0", SimpleJson.JsonEncode(decimal.MaxValue));
            //Assert.AreEqual("-79228162514264337593543950335.0", SimpleJson.JsonEncode(decimal.MinValue));
        }

        [TestMethod]
        public void FloatSerialization()
        {
            Assert.AreEqual("1.1", SimpleJson.SerializeObject(1.1));
            Assert.AreEqual("1.11", SimpleJson.SerializeObject(1.11));
            Assert.AreEqual("1.111", SimpleJson.SerializeObject(1.111));
            Assert.AreEqual("1.1111", SimpleJson.SerializeObject(1.1111));
            Assert.AreEqual("1.11111", SimpleJson.SerializeObject(1.11111));
            Assert.AreEqual("1.111111", SimpleJson.SerializeObject(1.111111));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1.0));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1d));
            //Assert.AreEqual("-1.0", SimpleJson.JsonEncode(-1d));
            Assert.AreEqual("1.01", SimpleJson.SerializeObject(1.01));
            Assert.AreEqual("1.001", SimpleJson.SerializeObject(1.001));
            //Assert.AreEqual(JsonConvert.PositiveInfinity, SimpleJson.JsonEncode(double.PositiveInfinity));
            //Assert.AreEqual(JsonConvert.NegativeInfinity, SimpleJson.JsonEncode(double.NegativeInfinity));
            //Assert.AreEqual(JsonConvert.NaN, SimpleJson.JsonEncode(double.NaN));
        }

        [TestMethod]
        public void EmptyObjectSerialization()
        {
            Assert.AreEqual("{}", SimpleJson.SerializeObject(new object()));
        }

        [TestMethod]
        public void CanParseWithUnicode()
        {
            var dog = new { Name = "Ăbbey" };

            var serialized = SimpleJson.SerializeObject(dog);

            var deserialized = (IDictionary<string, object>)SimpleJson.DeserializeObject(serialized);

            Assert.AreEqual(dog.Name, deserialized["Name"]);
        }

        [TestMethod]
        public void CanSerializeArrays()
        {
            const string expected = "[1,2,3]";

            var data = new[] { 1, 2, 3 };
            var serialized = SimpleJson.SerializeObject(data);

            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void CanSerializeEmptyArray()
        {
            const string expected = "[]";

            var data = new int[0];
            var serialized = SimpleJson.SerializeObject(data);

            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void CanSerializeNullArray()
        {
            const string expected = "null";

            int[] data = null;
            var serialized = SimpleJson.SerializeObject(data);

            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void CanSerializeList()
        {
            const string expected = "[\"a\",\"b\",\"c\"]";

            var data = new List<string> { "a", "b", "c" };
            var serialized = SimpleJson.SerializeObject(data);

            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        [Ignore]
        public void CanIgnoreSolidusInStringLiterals()
        {
            const string expected = @"What is the phone #/digits?";

            var serialized = SimpleJson.SerializeObject(
                new
                {
                    Value = @"What is the phone #\/digits?"
                });

            var actual = (IDictionary<string, object>)SimpleJson.DeserializeObject(serialized);

            Assert.AreEqual(expected, actual["Value"]);
        }

        [TestMethod]
        public void SerializeUnicodeTests()
        {
            var o = SimpleJson.SerializeObject("न");

            Assert.AreEqual("\"न\"", o);
        }

        [TestMethod]
        public void SerializeDictionaryStringObjectTests()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["caption"] = string.Empty;
            parameters["description"] = "hello world";
            parameters["link"] = "http://google.com";

            var result = SimpleJson.SerializeObject(parameters);

            Assert.AreEqual("{\"caption\":\"\",\"description\":\"hello world\",\"link\":\"http://google.com\"}", result);
        }

        [TestMethod]
        public void SerializeDictionaryStringStringTests()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["caption"] = string.Empty;
            parameters["description"] = "hello world";
            parameters["link"] = "http://google.com";

            var result = SimpleJson.SerializeObject(parameters);

            Assert.AreEqual("{\"caption\":\"\",\"description\":\"hello world\",\"link\":\"http://google.com\"}", result);
        }

        [TestMethod]
        public void SerializeSurrogatePair()
        {
            string str = "𩸽 is Arabesque greenling(fish) in japanese";
            string json = SimpleJson.SerializeObject(str);

            Assert.AreEqual("\"𩸽 is Arabesque greenling(fish) in japanese\"", json);
        }

        [TestMethod]
        public void SerializeDoubleQuotesCorrectly()
        {
            var obj = new { message = "Hi \"Prabir\"" };
            string json = SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"message\":\"Hi \\\"Prabir\\\"\"}", json);
        }

        [TestMethod]
        public void SerializeBigNumberCorrectly()
        {
            string json = SimpleJson.SerializeObject(new { object_id = 10150098461530576 });

            Assert.AreEqual("{\"object_id\":10150098461530576}", json);
        }

        [TestMethod]
        public void SerializeNullableTypeThatIsNotNull()
        {
            var obj = new Dictionary<string, object>();
            int? value = 3;
            obj["value"] = value;
            string json = SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"value\":3}", json);
        }

        [TestMethod]
        public void SerializeNullableTypeThatIsNull()
        {
            var obj = new Dictionary<string, object>();
            int? value = null;
            obj["value"] = value;
            string json = SimpleJson.SerializeObject(obj);

            Assert.AreEqual("{\"value\":null}", json);
        }
    }
}