using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.ArrangeActAssert;
using TinyBDDTests.Extensions;

namespace TinyBDDTests.Dsl.ArrangeActAssert
{
    [TestFixture]
    public class ArrangeActAssertDslTests
    {
        AAASpecialCase semanticModel;

        [SetUp]
        public void Setup()
        {
            semanticModel = AAA.New(aaa =>
            {
                string email = null;

                aaa.Arrange(() => email = "mail_a_goeran.no");

                aaa.Act(() => email = email.Replace("_a_", "@"));

                aaa.Assert(() => Assert.AreEqual("mail@goeran.no", email));
            });
        }

        [Test]
        public void Should_contain_arrange()
        {
            semanticModel.State.Arranges.Count.ShouldBe(1);
        }

        [Test]
        public void Should_contain_act()
        {
            semanticModel.State.Acts.Count.ShouldBe(1);
        }

        [Test]
        public void Should_contain_assert()
        {
            semanticModel.State.Acts.Values.Count.ShouldBe(1);
        }

        [Test]
        public void Should_be_able_to_execute()
        {
            semanticModel.Execute();
        }


    }
}
