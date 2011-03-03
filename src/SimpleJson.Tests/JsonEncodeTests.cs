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
    public class JsonEncodeTests
    {
        [TestMethod]
        public void GuidEncode()
        {
            Guid guid = new Guid("BED7F4EA-1A96-11d2-8F08-00A0C9A6186D");
            var json = SimpleJson.JsonEncode(guid);

            Assert.AreEqual(@"""bed7f4ea-1a96-11d2-8f08-00a0c9a6186d""", json);
        }

        [TestMethod]
        public void EnumEncode()
        {
            string json = SimpleJson.JsonEncode(StringComparison.CurrentCultureIgnoreCase);
            Assert.AreEqual("1", json);
        }

        [TestMethod]
        public void ObjectEncode()
        {
            object value;

            value = 1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = 1.1;
            Assert.AreEqual("1.1", SimpleJson.JsonEncode(value));

            value = 1.1m;
            Assert.AreEqual("1.1", SimpleJson.JsonEncode(value));

            //value = (float)1.1;
            //Assert.AreEqual("1.1", SimpleJson.JsonEncode(value));

            value = (short)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = (long)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = (byte)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = (uint)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = (ushort)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = (sbyte)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = (ulong)1;
            Assert.AreEqual("1", SimpleJson.JsonEncode(value));

            value = null;
            Assert.AreEqual("null", SimpleJson.JsonEncode(value));

            //value = DBNull.Value;
            //Assert.AreEqual("null", SimpleJson.JsonEncode(value));

            value = "I am a string";
            Assert.AreEqual(@"""I am a string""", SimpleJson.JsonEncode(value));

            value = true;
            Assert.AreEqual("true", SimpleJson.JsonEncode(value));

            value = false;
            Assert.AreEqual("false", SimpleJson.JsonEncode(value));

            //value = 'c';
            //Assert.AreEqual(@"""c""", SimpleJson.JsonEncode(value));
        }

        [TestMethod]
        public void FloatEncode()
        {
            Assert.AreEqual("1.1", SimpleJson.JsonEncode(1.1));
            Assert.AreEqual("1.11", SimpleJson.JsonEncode(1.11));
            Assert.AreEqual("1.111", SimpleJson.JsonEncode(1.111));
            Assert.AreEqual("1.1111", SimpleJson.JsonEncode(1.1111));
            Assert.AreEqual("1.11111", SimpleJson.JsonEncode(1.11111));
            Assert.AreEqual("1.111111", SimpleJson.JsonEncode(1.111111));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1.0));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1d));
            //Assert.AreEqual("-1.0", SimpleJson.JsonEncode(-1d));
            Assert.AreEqual("1.01", SimpleJson.JsonEncode(1.01));
            Assert.AreEqual("1.001", SimpleJson.JsonEncode(1.001));
            //Assert.AreEqual(JsonConvert.PositiveInfinity, SimpleJson.JsonEncode(double.PositiveInfinity));
            //Assert.AreEqual(JsonConvert.NegativeInfinity, SimpleJson.JsonEncode(double.NegativeInfinity));
            //Assert.AreEqual(JsonConvert.NaN, SimpleJson.JsonEncode(double.NaN));
        }

        [TestMethod]
        public void DecimalEncode()
        {
            Assert.AreEqual("1.1", SimpleJson.JsonEncode(1.1m));
            Assert.AreEqual("1.11", SimpleJson.JsonEncode(1.11m));
            Assert.AreEqual("1.111", SimpleJson.JsonEncode(1.111m));
            Assert.AreEqual("1.1111", SimpleJson.JsonEncode(1.1111m));
            Assert.AreEqual("1.11111", SimpleJson.JsonEncode(1.11111m));
            Assert.AreEqual("1.111111", SimpleJson.JsonEncode(1.111111m));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1.0m));
            //Assert.AreEqual("-1.0", SimpleJson.JsonEncode(-1.0m));
            //Assert.AreEqual("-1.0", SimpleJson.JsonEncode(-1m));
            //Assert.AreEqual("1.0", SimpleJson.JsonEncode(1m));
            Assert.AreEqual("1.01", SimpleJson.JsonEncode(1.01m));
            Assert.AreEqual("1.001", SimpleJson.JsonEncode(1.001m));
            //Assert.AreEqual("79228162514264337593543950335.0", SimpleJson.JsonEncode(decimal.MaxValue));
            //Assert.AreEqual("-79228162514264337593543950335.0", SimpleJson.JsonEncode(decimal.MinValue));
        }

        [TestMethod]
        public void StringEscpaingEncode()
        {
            string v = @"It's a good day
""sunshine""";

            string json = SimpleJson.JsonEncode(v);
            Assert.AreEqual(@"""It's a good day\r\n\""sunshine\""""", json);
        }

    }
}