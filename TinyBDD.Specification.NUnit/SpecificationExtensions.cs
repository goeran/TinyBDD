using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.Templates;
using System.Collections;

namespace TinyBDD.Specification.NUnit
{
    public static class TestExtensions
    {
        public static void ShouldBe(this object anObj, object value)
        {
            Assert.AreEqual(value, anObj);
        }

        public static void ShouldNotBe(this object valueToTest, object expected)
        {
            Assert.AreNotEqual(expected, valueToTest);
        }

        #region double
        public static void ShouldBeGreaterThan(this double value, double expected)
        {
            Assert.Greater(value, expected);
        }

        public static void ShouldBeLessThan(this double value, double expected)
        {
            Assert.Less(value, expected);
        }
        #endregion

        #region int
        public static void ShouldBeLessThan(this int value, int expected)
        {
            Assert.Less(value, expected);
        }
        #endregion

        public static void ShouldNotBeNull(this object anObj)
        {
            Assert.IsNotNull(anObj);
        }

        public static void ShouldBeNull(this object valueToTest)
        {
            Assert.IsNull(valueToTest);
        }

        public static void ShouldBeSameAs(this object valueToTest, object expected)
        {
            Assert.AreSame(valueToTest, expected);
        }

        public static void ShouldNotBeSameAs(this object valueToTest, object expected)
        {
            Assert.AreNotSame(valueToTest, expected);
        }

        public static void ShouldBeTrue(this bool valueToTest)
        {
            Assert.IsTrue(valueToTest);
        }

        public static void ShouldBeFalse(this bool valueToTest)
        {
            Assert.IsFalse(valueToTest);
        }

        public static void ShouldBeInstanceOfType<T>(this object valueToTest)
        {
            Assert.IsInstanceOfType(typeof(T), valueToTest);
        }

        public static void ShouldNotBeInstanceOfType<T>(this object valueToTest)
        {
            Assert.IsNotInstanceOfType(typeof(T), valueToTest);
        }

        public static void ShouldBeEmpty(this IList list)
        {
            Assert.AreEqual(0, list.Count);
        }

        public static void ShouldContain(this string str, string expectedStr)
        {
            Assert.IsTrue(str.Contains(expectedStr), string.Format("string \"{0}\" should contained \"{1}\"", str, expectedStr));
        }

        public static void ShouldContain(this IEnumerable list, object expectedObject)
        {
            foreach (var item in list)
            {
                if (item == expectedObject)
                    return;
            }
            Assert.Fail("Expected object was not in list");
        }

        public static void ShouldHave(this IList list, int expectedCount)
        {
            Assert.AreEqual(expectedCount, list.Count);
        }

        public static void ShouldHaveMoreThan(this IList list, int expectedCount)
        {
            Assert.Greater(list.Count, expectedCount);
        }

        public static void ShouldThrowException<T>(this object anObj, Action action, Action<T> exception) where T : Exception
        {
            ShouldThrowExceptionTemplate<T> template = new ShouldThrowExceptionTemplate<T>(
                () => action.Invoke(), m => Assert.Fail(m), exception);
            template.Execute();

        }
    }
}
