
namespace SimpleJsonTests
{
    using System;
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

    using SimpleJson;

    [TestClass]
    public class SimpleJsonTests
    {
        [TestMethod]
        public void CanParseSimpleExample()
        {
            const string json = @"{ ""name"" : ""spot"" }";

            var result = (IDictionary<string, object>)SimpleJson.JsonDecode(json);

            Assert.AreEqual("spot", result["name"]);
            Assert.IsInstanceOf<string>(result["name"]);
        }

        [TestMethod]
        public void CanParseUnicodeLiteralsAndSymbols()
        {
            const string json = "{ \"literal\": \"\\u03a0\", \"symbol\": \"\x3a0\" }";

            var result = (IDictionary<string, object>)SimpleJson.JsonDecode(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(result["literal"], result["symbol"]);
        }

        [TestMethod]
        public void CanParseControlCharactersAsWhiteSpace()
        {
            const string json = "[\t\r\b\f\n{\"color\": \"red\",\"value\": \"#f00\"}]";

            var result = (IList<object>)SimpleJson.JsonDecode(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanParseArrays()
        {
            const string json = @"[{""color"": ""red"",""value"": ""#f00""}]";

            var result = (IList<object>)SimpleJson.JsonDecode(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var obj = (IDictionary<string, object>)result[0];
            Assert.AreEqual(2, obj.Keys.Count);
            Assert.AreEqual("red", obj["color"]);
            Assert.AreEqual("#f00", obj["value"]);
        }

        [TestMethod]
        public void CanParseKeywords()
        {
            const string json = @"{ ""yay"" : true, ""nay"": false, ""nada"": null }";

            var result = (IDictionary<string, object>)SimpleJson.JsonDecode(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result["yay"]);
            Assert.AreEqual(false, result["nay"]);
            Assert.AreEqual(null, result["nada"]);
        }

        [TestMethod]
        public void CanParseNumbers()
        {
            const string json = @"{""quantity"":8902,""cost"":45.33,""value"":-1.063E-02}";

            var result = (IDictionary<string, object>)SimpleJson.JsonDecode(json);

            Assert.IsNotNull(result);

            Assert.IsInstanceOf<double>(result["quantity"]);
            Assert.AreEqual(8902, result["quantity"]);

            Assert.IsInstanceOf<double>(result["cost"]);
            Assert.AreEqual(45.33, result["cost"]);

            Assert.IsInstanceOf<double>(result["value"]);
            Assert.AreEqual(-1.063E-02, result["value"]);
        }

        [TestMethod]
        public void CanSerializeClassInstanceExample()
        {
            const string expected = @"{""Name"":""spot""}";
            var dog = new Dog { Name = "spot" };

            var json = SimpleJson.JsonEncode(dog);

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeAnonymousObject()
        {
            const string expected = @"{""name"":""spot""}";
            var instance = new { name = "spot" };

            var json = SimpleJson.JsonEncode(instance);

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeAnonymousObjectWithNumbers()
        {
            // todo: make json encode smaller by removing spaces after ,
            const string expected = @"{""quantity"":8902, ""cost"":45.33, ""value"":-0.01063}";
            var instance = new
            {
                quantity = 8902,
                cost = 45.33,
                value = -1.063E-02
            };

            var json = SimpleJson.JsonEncode(instance);
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeWithAnonymousTypes()
        {
            const string expected = @"{""array"":{""quantity"":8902, ""cost"":45.33, ""value"":-0.01063}}";
            var instance = new
            {
                array = new { quantity = 8902, cost = 45.33, value = -1.063E-02 }
            };

            var json = SimpleJson.JsonEncode(instance);
            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void CanSerializeWithDates()
        {
            var instance = new
            {
                now = DateTime.UtcNow
            };

            var json = SimpleJson.JsonEncode(instance);
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void CanParseWithUnicode()
        {
            var dog = new { Name = "Ăbbey" };

            var serialized = SimpleJson.JsonEncode(dog);

            Console.WriteLine(serialized);

            var deserialized = (IDictionary<string, object>)SimpleJson.JsonDecode(serialized);

            Assert.AreEqual(dog.Name, deserialized["Name"]);
        }

        [TestMethod]
        public void CanIgnoreSolidusInStringLiterals()
        {
            const string expected = @"What is the phone #/digits?";

            var serialized = SimpleJson.JsonEncode(
                new
                    {
                        Value = @"What is the phone #\/digits?"
                    });

            var actual = (IDictionary<string, object>)SimpleJson.JsonDecode(serialized);

            Assert.AreEqual(expected, actual["Value"]);
        }
    }

    public class Dog
    {
        public string Name { get; set; }
    }
}