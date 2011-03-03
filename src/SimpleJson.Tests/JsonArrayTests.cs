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
    public class JsonArrayTests
    {
        [TestMethod]
        public void Clear()
        {
            JsonArray a = new JsonArray { 1 };
            Assert.AreEqual(1, a.Count);

            a.Clear();
            Assert.AreEqual(0, a.Count);
        }

        [TestMethod]
        public void Contains()
        {
            int v = 1;

            JsonArray a = new JsonArray { v };

            Assert.IsFalse(a.Contains(2));
            Assert.IsFalse(a.Contains(null));
            Assert.IsTrue(a.Contains(v));
        }

        [TestMethod]
        public void Remove()
        {
            object v = 1;
            JsonArray j = new JsonArray();
            j.Add(v);

            Assert.AreEqual(1, j.Count);

            Assert.AreEqual(false, j.Remove(2));
            Assert.AreEqual(false, j.Remove(null));
            Assert.AreEqual(true, j.Remove(v));
            Assert.AreEqual(false, j.Remove(v));

            Assert.AreEqual(0, j.Count);
        }

        [TestMethod]
        public void IndexOf()
        {
            object v1 = 1;
            object v2 = 2;
            object v3 = 3;

            JsonArray j = new JsonArray();
            j.Add(v1);

            Assert.AreEqual(0, j.IndexOf(v1));

            j.Add(v2);
            Assert.AreEqual(0, j.IndexOf(v1));
            Assert.AreEqual(1, j.IndexOf(v2));
        }

        [TestMethod]
        public void RemoveAt()
        {
            object v1 = 1;
            object v2 = 2;
            object v3 = 3;

            JsonArray j = new JsonArray();
            j.Add(v1);
            j.Add(v2);
            j.Add(v3);

            Assert.AreEqual(true, j.Contains(v1));
            j.RemoveAt(0);
            Assert.AreEqual(false, j.Contains(v1));

            Assert.AreEqual(true, j.Contains(v3));
            j.RemoveAt(1);
            Assert.AreEqual(false, j.Contains(v3));

            Assert.AreEqual(1, j.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = @"Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index")]
        public void RemoveAtOutOfRangeIndexShouldBeError()
        {
            JsonArray j = new JsonArray();
            j.RemoveAt(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = @"Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index")]
        public void RemoveNegtiveIndexShouldBeError()
        {
            JsonArray j = new JsonArray();
            j.RemoveAt(-1);
        }

        [TestMethod]
        public void Insert()
        {
            object v1 = 1;
            object v2 = 2;
            object v3 = 3;
            object v4 = 4;

            JsonArray j = new JsonArray();

            j.Add(v1);
            j.Add(v2);
            j.Add(v3);
            j.Insert(1, v4);

            Assert.AreEqual(0, j.IndexOf(v1));
            Assert.AreEqual(1, j.IndexOf(v4));
            Assert.AreEqual(2, j.IndexOf(v2));
            Assert.AreEqual(3, j.IndexOf(v3));
        }

        [TestMethod]
        public void InsertShouldInsertAtZeroIndex()
        {
            object v1 = 1;
            object v2 = 2;

            JsonArray j = new JsonArray();

            j.Insert(0, v1);
            Assert.AreEqual(0, j.IndexOf(v1));

            j.Insert(0, v2);
            Assert.AreEqual(1, j.IndexOf(v1));
            Assert.AreEqual(0, j.IndexOf(v2));
        }

        [TestMethod]
        public void InsertNull()
        {
            JsonArray j = new JsonArray();
            j.Insert(0, null);

            Assert.AreEqual(null, j[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = @"Index must be within the bounds of the List.
Parameter name: index")]
        public void InsertNegativeIndexShouldThrow()
        {
            JsonArray j = new JsonArray();
            j.Insert(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = @"Index must be within the bounds of the List.
Parameter name: index")]
        public void InsertOutOfRangeIndexShouldThrow()
        {
            JsonArray j = new JsonArray();
            j.Insert(2, 1);
        }

        [TestMethod]
        public void Item()
        {
            object v1 = 1;
            object v2 = 2;
            object v3 = 3;
            object v4 = 4;

            JsonArray j = new JsonArray();

            j.Add(v1);
            j.Add(v2);
            j.Add(v3);

            j[1] = v4;

            Assert.AreEqual(-1, j.IndexOf(v2));
            Assert.AreEqual(1, j.IndexOf(v4));
        }

        [TestMethod]
        public void Iterate()
        {
            JsonArray a = new JsonArray { 1, 2, 3, 4, 5 };

            int i = 1;
            foreach (object o in a)
            {
                Assert.AreEqual(i, o);
                ++i;
            }
        }
    }
}