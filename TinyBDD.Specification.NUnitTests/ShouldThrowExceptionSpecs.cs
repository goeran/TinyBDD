using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TinyBDD.Specification.NUnit;

namespace TinyBDD.SpecificationTests.NUnit.SpecificationExtensionsSpecs
{
    [TestFixture]
    public class ShouldThrowExceptionSpecs
    {
        [Test]
        public void Should_be_able_to_use_simple_specificaiton()
        {
            this.ShouldThrowException<ArgumentNullException>(() => { throw new ArgumentNullException(); });
        }

        [Test]
        public void Should_throw_AssertionException_if_specified_exception_is_not_thrown()
        {
            bool exceptionThrown = false;
            try
            {
                this.ShouldThrowException<Exception>(() => { }, ex => { });
            }
            catch (AssertionException ex)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown, "AssertionException was not thrown");
        }

        [Test]
        public void Should_not_throw_AssertionException_if_specified_exception_is_thrown()
        {
            bool exceptionThrown = false;

            try
            {
                this.ShouldThrowException<ArgumentNullException>(() =>
                {
                    throw new ArgumentNullException();
                }, ex => {});
            }
            catch (AssertionException)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown, "AssertionException was not thrown");
        }
    }
}
