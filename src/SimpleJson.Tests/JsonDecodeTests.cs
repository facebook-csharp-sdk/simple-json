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
    public class JsonDecodeTests
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

            Assert.IsInstanceOf<IDictionary<string, object>>(obj);
            Assert.IsInstanceOf<JsonObject>(obj);

            var root = (IDictionary<string, object>)obj;

            Assert.AreEqual(2, root.Count);

            Assert.IsTrue(root.ContainsKey("CPU"));
            Assert.AreEqual("Intel", root["CPU"]);

            Assert.IsTrue(root.ContainsKey("Drives"));

            Assert.IsInstanceOf<IList<object>>(root["Drives"]);
            Assert.IsInstanceOf<JsonArray>(root["Drives"]);
            var drives = (IList<object>)root["Drives"];

            Assert.AreEqual(2, drives.Count);

            Assert.IsInstanceOf<string>(drives[0]);
            Assert.AreEqual("DVD read/writer", drives[0]);

            Assert.IsInstanceOf<string>(drives[1]);
            Assert.AreEqual("500 gigabyte hard drive", drives[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException), ExpectedMessage = "Invalid JSON string")]
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
        [ExpectedException(typeof(SerializationException), ExpectedMessage = "Invalid JSON string")]
        public void UnexpectedEndOfHex()
        {
            var result = SimpleJson.DeserializeObject(@"'h\u006");
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException), ExpectedMessage = "Invalid JSON string")]
        public void UnexpectedEndOfControlCharacter()
        {
            var result = SimpleJson.DeserializeObject(@"'h\");
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException), ExpectedMessage = "Invalid JSON string")]
        public void UnexpectedEndWhenParsingUnquotedProperty()
        {
            var result = SimpleJson.DeserializeObject(@"{aww");
        }

        [TestMethod]
        public void ParsingQuotedPropertyWithControlCharacters()
        {
            var result = SimpleJson.DeserializeObject("{\"hi\r\nbye\":1}");

            Assert.IsInstanceOf<IDictionary<string, object>>(result);
            Assert.IsInstanceOf<JsonObject>(result);

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

            Assert.IsInstanceOf<IDictionary<string, object>>(o);
            var dict = (IDictionary<string, object>)o;

            Assert.AreEqual("Intel", dict["CPU"]);
        }

        [TestMethod]
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

            Assert.IsInstanceOf<string>(a[0]);
            Assert.AreEqual(20000, ((string)a[0]).Length);
        }

        [TestMethod]
        public void EscapedUnicodeTests()
        {
            string json = @"[""\u003c"",""\u5f20""]";

            var o = SimpleJson.DeserializeObject(json);

            Assert.IsInstanceOf<List<object>>(o);

            var l = (List<object>)o;
            Assert.AreEqual(2, l.Count);

            Assert.AreEqual("<", l[0]);
            //Assert.AreEqual("24352", Convert.ToInt32(Convert.ToChar(l[0])));
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException), ExpectedMessage = "Invalid JSON string")]
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

            Assert.IsInstanceOf<IDictionary<string, object>>(o);
            Assert.IsInstanceOf<JsonObject>(o);

            var dict = (IDictionary<string, object>)o;

            Assert.AreEqual(@"Hi,I" + '\u0092' + "ve send you smth", dict["Message"]);
        }

        [TestMethod]
        public void ReadHexidecimalWithAllLetters()
        {
            string json = @"{""text"":0xabcdef12345}";

            var o = SimpleJson.DeserializeObject(json);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException), ExpectedMessage = "Invalid JSON string")]
        public void ParseIncompleteArray()
        {
            var o = SimpleJson.DeserializeObject("[1");
        }
    }
}