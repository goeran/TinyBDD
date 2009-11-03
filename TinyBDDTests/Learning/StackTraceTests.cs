using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;

using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.Learning
{
    [TestFixture]
    public class StackTraceTests
    {
        [Test]
        public void Get_callers_method_name()
        {
            StackTrace a = new StackTrace();
            var frames = a.GetFrames();
            var parentMethod = frames.First().GetMethod();
            Console.WriteLine(parentMethod.Name);
            parentMethod.Name.ShouldBe("Get_callers_method_name");
        }
    }
}
