using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD;

namespace TinyBDDTests.Dsl
{
    [TestFixture]
    public class ComplexScenarioTests
    {
        Scenario builtScenario;
        string output;

        [SetUp]
        public void Setup()
        {
            output = "";
            builtScenario = Scenario.New("Account is in credit", scenario =>
                {
                    scenario.
                        Given("the account is in credit", () =>
                            output += "C").
                        And("the card is valid", () =>
                            output += "C").
                        And("the dispenser contains cash", () =>
                            output += "C");

                    scenario.
                        When("the customer request cash", () =>
                            output += "E").
                        Then("ensure the account is debited", () =>
                            output += "O").
                        And("ensure cash is dispensed", () =>
                            output += "O").
                        And("ensure the card is returned", () =>
                            output += "O");
                }).End();

        }

        [Test]
        public void Should_contain_an_context()
        {
            builtScenario.Context.Count.ShouldEqual(3);
        }

        [Test]
        public void Should_contain_an_event()
        {
            builtScenario.Events.ShouldNotBeNull();
        }

        [Test]
        public void Should_be_able_to_run_scenario()
        {
            builtScenario.Run();
            output.ShouldEqual("CCCEOOO");
        }
    }
}
