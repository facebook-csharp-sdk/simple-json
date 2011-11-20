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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

    using SimpleJson;

    [TestClass]
    public class DeserializeObjectTests
    {

        [TestMethod]
        public void ReadIndented()
        {
            string input = @"{
  ""CPU"": ""Intel"",
  ""Drives"": [
    ""DVD read/writer"",
    ""500 gigabyte hard drive""
  ]
}";
            object obj = SimpleJson.DeserializeObject(input);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(obj, typeof(IDictionary<string, object>));
            Assert.IsInstanceOfType(obj, typeof(JsonObject));
#else
            Assert.IsInstanceOf<IDictionary<string, object>>(obj);
            Assert.IsInstanceOf<JsonObject>(obj);
#endif

            var root = (IDictionary<string, object>)obj;

            Assert.AreEqual(2, root.Count);

            Assert.IsTrue(root.ContainsKey("CPU"));
            Assert.AreEqual("Intel", root["CPU"]);

            Assert.IsTrue(root.ContainsKey("Drives"));

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(root["Drives"], typeof(IList<object>));
            Assert.IsInstanceOfType(root["Drives"], typeof(JsonArray));
#else
            Assert.IsInstanceOf<IList<object>>(root["Drives"]);
            Assert.IsInstanceOf<JsonArray>(root["Drives"]);
#endif

            var drives = (IList<object>)root["Drives"];

            Assert.AreEqual(2, drives.Count);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(drives[0], typeof(string));
#else
            Assert.IsInstanceOf<string>(drives[0]);
#endif
            Assert.AreEqual("DVD read/writer", drives[0]);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(drives[1], typeof(string));
#else
            Assert.IsInstanceOf<string>(drives[1]);
#endif
            Assert.AreEqual("500 gigabyte hard drive", drives[1]);
        }



        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void UnexpectedEndOfString()
        {
            var result = SimpleJson.DeserializeObject("hi");
        }

        [TestMethod]
        public void ReadNullTerminiatorString()
        {
            var result = SimpleJson.DeserializeObject("\"h\0i\"");

            Assert.AreEqual("h\0i", result);
        }


        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void UnexpectedEndOfHex()
        {
            var result = SimpleJson.DeserializeObject(@"'h\u006");
        }


        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void UnexpectedEndOfControlCharacter()
        {
            var result = SimpleJson.DeserializeObject(@"'h\");
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void UnexpectedEndWhenParsingUnquotedProperty()
        {
            var result = SimpleJson.DeserializeObject(@"{aww");
        }


        [TestMethod]
        public void ParsingQuotedPropertyWithControlCharacters()
        {
            var result = SimpleJson.DeserializeObject("{\"hi\r\nbye\":1}");

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(result, typeof(IDictionary<string, object>));
            Assert.IsInstanceOfType(result, typeof(JsonObject));
#else
            Assert.IsInstanceOf<IDictionary<string, object>>(result);
            Assert.IsInstanceOf<JsonObject>(result);
#endif


            var dict = (IDictionary<string, object>)result;

            foreach (KeyValuePair<string, object> pair in dict)
            {
                Assert.AreEqual(@"hi
bye", pair.Key);
            }
        }



        [TestMethod]
        public void ReadNewLineLastCharacter()
        {
            string input = @"{
  ""CPU"": ""Intel"",
  ""Drives"": [ 
    ""DVD read/writer"",
    ""500 gigabyte hard drive""
  ]
}" + '\n';

            object o = SimpleJson.DeserializeObject(input);
            Assert.IsNotNull(o);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(o, typeof(IDictionary<string, object>));
#else
            Assert.IsInstanceOf<IDictionary<string, object>>(o);
#endif


            var dict = (IDictionary<string, object>)o;

            Assert.AreEqual("Intel", dict["CPU"]);
        }



        [TestMethod]
#if SIMPLE_JSON_WINRT
        [Ignore]
#else
        [Ignore("not part of the json standard.")]
#endif
        public void FloatingPointNonFiniteNumber()
        {
            string input = @"[
  NaN,
  Infinity,
  -Infinity
]";
            var o = SimpleJson.DeserializeObject(input);
        }

        [TestMethod]
        public void LongStringTests()
        {
            int length = 20000;
            string json = @"[""" + new string(' ', length) + @"""]";

            var o = SimpleJson.DeserializeObject(json);

            var a = (IList<object>)o;

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(a[0], typeof(string));
#else
            Assert.IsInstanceOf<string>(a[0]);
#endif


            Assert.AreEqual(20000, ((string)a[0]).Length);
        }

        [TestMethod]
        public void EscapedUnicodeTests()
        {
            string json = @"[""\u003c"",""\u5f20""]";

            var o = SimpleJson.DeserializeObject(json);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(o, typeof(List<object>));
#else
            Assert.IsInstanceOf<List<object>>(o);
#endif

            var l = (List<object>)o;
            Assert.AreEqual(2, l.Count);

            Assert.AreEqual("<", l[0]);
            //Assert.AreEqual("24352", Convert.ToInt32(Convert.ToChar(l[0])));
        }



        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void MissingColon()
        {
            string json = @"{
    ""A"" : true,
    ""B"" ""hello"", // Notice the colon is missing
    ""C"" : ""bye""
}";

            var o = SimpleJson.DeserializeObject(json);
        }



        [TestMethod]
#if SIMPLE_JSON_WINRT
        [Ignore]
#else
        [Ignore("not part of the json standard.")]
#endif
        public void ReadOcatalNumber()
        {
            var json = @"[0372, 0xFA, 0XFA]";

            var o = SimpleJson.DeserializeObject(json);
        }


        [TestMethod]
        public void ReadUnicode()
        {
            string json = @"{""Message"":""Hi,I\u0092ve send you smth""}";

            var o = SimpleJson.DeserializeObject(json);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(o, typeof(IDictionary<string, object>));
            Assert.IsInstanceOfType(o, typeof(JsonObject));
#else
            Assert.IsInstanceOf<IDictionary<string, object>>(o);
            Assert.IsInstanceOf<JsonObject>(o);
#endif



            var dict = (IDictionary<string, object>)o;

            Assert.AreEqual(@"Hi,I" + '\u0092' + "ve send you smth", dict["Message"]);
        }


        [TestMethod]
        public void DeserializeUnicodeChar()
        {
            string json = "\"न\"";

            var o = SimpleJson.DeserializeObject(json);

#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(o, typeof(string));
#else
            Assert.IsInstanceOf<string>(o);
#endif



            Assert.AreEqual("न", o);
        }

        [TestMethod]
#if SIMPLE_JSON_WINRT
        [Ignore]
#else
        [Ignore("not part of the json standard.")]
#endif
        public void ReadHexidecimalWithAllLetters()
        {
            string json = @"{""text"":0xabcdef12345}";

            var o = SimpleJson.DeserializeObject(json);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void ParseIncompleteArray()
        {
            var o = SimpleJson.DeserializeObject("[1");
        }

        [TestMethod]
        public void DeserializeSurrogatePair()
        {
            string json = "\"𩸽 is Arabesque greenling(fish) in japanese\"";
            var o = SimpleJson.DeserializeObject(json);
#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(o, typeof(string));
#else
            Assert.IsInstanceOf<string>(o);
#endif

            Assert.AreEqual("𩸽 is Arabesque greenling(fish) in japanese", o);
        }

        [TestMethod]
        public void DeserializeEscapedSurrogatePair()
        {
            string json = "\"\\uD867\\uDE3D is Arabesque greenling(fish)\"";  // 𩸽
            var o = SimpleJson.DeserializeObject(json);
#if SIMPLE_JSON_WINRT
            Assert.IsInstanceOfType(o, typeof(string));
#else
            Assert.IsInstanceOf<string>(o);
#endif
            Assert.AreEqual("\uD867\uDE3D is Arabesque greenling(fish)", o);
            Assert.AreEqual("𩸽 is Arabesque greenling(fish)", o);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException),
#if !SIMPLE_JSON_WINRT
 ExpectedMessage =
#endif
 "Invalid JSON string")]
        public void DeserializeInvaildEscapedSurrogatePair()
        {
            string json = "\"\\uD867\\u0000 is Arabesque greenling(fish)\"";
            var o = SimpleJson.DeserializeObject(json);
        }

        [TestMethod]
        public void DeserializeKnownJsonObjectType()
        {
            var json = "{\"FirstName\":\"Bruno\",\"LastName\":\"Baïa\",\"Address\":{\"Country\":\"France\",\"City\":\"Toulouse\"}}";
            var result = SimpleJson.DeserializeObject<JsonObject>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("Bruno", result["FirstName"]);
            Assert.AreEqual("Baïa", result["LastName"]);
            var address = result["Address"] as JsonObject;
            Assert.AreEqual("France", address["Country"]);
            Assert.AreEqual("Toulouse", address["City"]);
        }

        [TestMethod]
        public void DeserializeKnownJsonArrayType()
        {
            var json = "[{\"name\":\"road cycling\",\"value\":11},{\"name\":\"football\",\"value\":9}]";
            var result = SimpleJson.DeserializeObject<JsonArray>(json);

            Assert.IsNotNull(result);
            foreach (JsonObject hobby in result)
            {
#if SIMPLE_JSON_WINRT
                Assert.IsNotNull(hobby["name"]);
                Assert.IsNotNull(hobby["value"]);
#else
                Assert.NotNull(hobby["name"]);
                Assert.NotNull(hobby["value"]);
#endif

            }
        }

        [TestMethod]
        public void DeserializeDoubleQuotesCorrectly()
        {
            var json = "{\"message\":\"Hi \\\"Prabir\\\"\"}";
            var result = (IDictionary<string, object>)SimpleJson.DeserializeObject(json);

            Assert.AreEqual("Hi \"Prabir\"", result["message"]);
        }
    }
}