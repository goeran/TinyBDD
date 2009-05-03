using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
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
            semanticModel = Scenario.New("Account is in credit", scenario =>
            {
                string output = "";

                scenario.Given("the account is in credit", () =>
                        output += "C").
                    And("the card is valid", () =>
                        output += "C").
                    And("the dispenser contains cash", () =>
                        output += "C");

                scenario.When("the customer request cash", () =>
                        output += "E");

                scenario.Then("ensure the account is debited", () =>
                        output += "O").
                    And("ensure cash is dispensed", () =>
                        output += "O").
                    And("ensure the card is returned", () =>
                        output += "O");
            });

        }

        [Test]
        public void SemanticModel_should_contain_arrange()
        {
            Assert.AreEqual(3, semanticModel.State.Arranges.Count);
        }

        [Test]
        public void SemanticModel_should_contain_act()
        {
            Assert.AreEqual(1, semanticModel.State.Acts.Count);
        }

        [Test]
        public void SemanticModel_should_contain_assert()
        {
            Assert.AreEqual(3, semanticModel.State.Asserts.Count);
        }

        [Test]
        public void Should_be_able_to_execute()
        {
            semanticModel.Execute();
        }
    }
}
