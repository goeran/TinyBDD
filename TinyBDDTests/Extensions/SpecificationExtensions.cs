using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TinyBDDTests.Extensions
{
    public static class SpecificationExtensions
    {
        public static void ShouldBe(this object obj, object expected)
        {
            Assert.AreEqual(expected, obj);
        }

        public static void ShouldNotBeNull(this object obj)
        {
            Assert.IsNotNull(obj);
        }

        public static void ShouldBeInstanceOfType<T>(this object obj)
        {
            Assert.IsInstanceOfType(typeof(T), obj);
        }

        public static void ShouldHave(this IList list, int expected)
        {
            Assert.AreEqual(expected, list.Count);
        }

        public static void StringShouldContain(this string str, string expectedStr)
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

        public static void ShouldThrowException<T>(this object anObj, Action action) where T : Exception
        {
            ShouldThrowException<T>(action, ex => { });
        }

        public static void ShouldThrowException<T>(this object anObj, Action action, Action<T> exception) where T : Exception
        {
            ShouldThrowException<T>(action, exception);
        }

        private static void ShouldThrowException<T>(Action action, Action<T> exception) where T : Exception
        {
            Exception ex = null;

            try
            {
                action();
            }
            catch (Exception e)
            {
                ex = e;
            }

            if (ex == null)
                Assert.Fail("Did not throw exception");
            else
            {
                if (ex.GetType() != typeof(T))
                    Assert.Fail(
                        string.Format("Did not throw expected exception ({0}), but {1}", typeof(T).Name, ex.GetType().Name));
                else
                    exception((T)ex);
            }
        }

        public static void ShouldBeTrue(this bool valueToTest)
        {
            Assert.IsTrue(valueToTest);
        }

        public static void ShouldBeFalse(this bool valueToTest)
        {
            Assert.IsFalse(valueToTest);
        }

    }
}
