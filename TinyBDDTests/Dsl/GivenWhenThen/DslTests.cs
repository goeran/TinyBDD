using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyBDDTests.Dsl.GivenWhenThen
{
    [TestFixture]
    public class DslTests
    {
        ScenarioSpecialCase semanticModel;

        [SetUp]
        public void Setup()
        {
            semanticModel = Scenario.New("String replacement", scenario =>
            {
                string email = null;
                scenario.Given("We have an invalid email address", () =>
                    email = "mail_a_goeran.no");

                scenario.When("invalid chars are replaced with a valid one", () =>
                    email = email.Replace("_a_", "@"));

                scenario.Then("", () =>
                    Assert.AreEqual("mail@goeran.no", email));
            });
        }

        [Test]
        public void SemanticModel_should_contain_arrange()
        {
            Assert.IsNotNull(semanticModel.State.Arrange);
        }

        [Test]
        public void SemanticModel_should_contain_act()
        {
            Assert.AreEqual(1, semanticModel.State.Acts.Count);
        }

        [Test]
        public void SemanticModel_should_contain_assert()
        {
            Assert.AreEqual(1, semanticModel.State.Asserts.Count);
        }

        [Test]
        public void Should_be_able_to_execute()
        {
            semanticModel.Execute();
        }
    }
}
