using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.Templates;

namespace TinyBDD.Specification.NUnit
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
            ShouldThrowExceptionTemplate<T> template = new ShouldThrowExceptionTemplate<T>(
                () => action.Invoke(), m => Assert.Fail(m), exception);
            template.Execute();
        }
    }
}
