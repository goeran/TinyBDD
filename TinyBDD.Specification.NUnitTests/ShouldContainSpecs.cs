using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;

namespace TinyBDD.Specification.NUnitTests
{
    [TestFixture]
    public class ShouldContainSpecs
    {
        private List<string> list;

        [SetUp]
        public void Setup()
        {
            list = new List<string>();
            list.Add("hello");
            list.Add("world");
        }

        [Test]
        public void Should_not_throw_AssertException_when_item_is_in_list()
        {
            list.ShouldContain("hello");
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void Should_throw_AssertException_when_item_is_not_in_list()
        {
            list.ShouldContain("not in list");
        }
    }
}
