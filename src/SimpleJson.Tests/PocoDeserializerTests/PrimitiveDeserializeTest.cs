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

namespace SimpleJsonTests.PocoDeserializerTests
{
	[TestClass]
	public class PrimitiveDeserializeTest
	{
		[TestMethod]
		public void SimpleIntTests()
		{
			string json = "{\"Age\":\"12\",\"Name\": \"Simple Json\"}";

			var result = SimpleJson.SimpleJson.DeserializeObject<X>(json);

			Assert.AreEqual(12, result.Age);
		}

		[TestMethod]
		public void SimpleDoubleTests()
		{
			string json = "{\"Salary\":\"120.50\",\"Name\": \"Simple Json\"}";

			var result = SimpleJson.SimpleJson.DeserializeObject<X>(json);

			Assert.AreEqual(120.50, result.Salary);
		}

		class X
		{
			public string Name { get; set; }
			public int Age { get; set; }
			public double Salary { get; set; }
		}
	}
}
