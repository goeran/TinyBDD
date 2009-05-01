﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD;
using TinyBDD.Dsl.ArrangeActAssert;

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
            semanticModel.State.Arranges.Count.ShouldEqual(1);
        }

        [Test]
        public void Should_contain_act()
        {
            semanticModel.State.Acts.Count.ShouldEqual(1);
        }

        [Test]
        public void Should_contain_assert()
        {
            semanticModel.State.Asserts.Count.ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_execute()
        {
            semanticModel.Execute();
        }


    }
}