
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
	public class EnumTypeTest
	{
		public enum TestValues
		{
			Dog,
			Cat,
			House,
			Roof
		}

		public enum TestValues2
		{
			one = 1,
			two = 4,
			three = 5
		};

		public class ObjProp
		{
			public ObjProp()
			{
			}

			public TestValues Test1 { get; set; }

			public TestValues2 Test2 { get; set; }
		}

		[TestMethod]
		public void SerializeEnum1()
		{
			var obj = new ObjProp ();
			obj.Test1 = TestValues.Cat;

			var str = SimpleJson.SerializeObject (obj);
			var rtn = SimpleJson.DeserializeObject<ObjProp> (str);

			Console.WriteLine (rtn.Test1);
			Assert.AreEqual(TestValues.Cat, rtn.Test1);
		}

		[TestMethod]
		public void SerializeEnum2()
		{
			var obj = new ObjProp ();
			obj.Test1 = TestValues.Roof;

			var str = SimpleJson.SerializeObject (obj);
			var rtn = SimpleJson.DeserializeObject<ObjProp> (str);

			Console.WriteLine (rtn.Test1);
			Assert.AreEqual(TestValues.Roof, rtn.Test1);
		}

		[TestMethod]
		public void SerializeEnum3()
		{
			var obj = new ObjProp ();
			obj.Test1 = TestValues.Roof;
			obj.Test2 = TestValues2.three;

			var str = SimpleJson.SerializeObject (obj);
			var rtn = SimpleJson.DeserializeObject<ObjProp> (str);

			Console.WriteLine (rtn.Test1);
			Console.WriteLine (rtn.Test2);
			Assert.AreEqual(TestValues.Roof, rtn.Test1);
			Assert.AreEqual(TestValues2.three, rtn.Test2);
		}
	}
}

