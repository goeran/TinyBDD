using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TinyBDD
{
    public static class TestExtensions
    {
        public static void ShouldEqual(this object anObj, object value)
        {
            Assert.AreEqual(value, anObj);
        }

        public static void ShouldNotBeNull(this object anObj)
        {
            Assert.IsNotNull(anObj);
        }

        public static void ShouldThrowException<T>(this object anObj, Action action, Action<T> exception) where T : Exception
        {
            Exception ex = null;

            try
            {
                action.Invoke();
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
                    Assert.Fail("Did not throw expected exception ({0}), but {1}");
                else
                    exception.Invoke((T)ex);
            }

        }
    }
}
