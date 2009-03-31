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
    }
}
