using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD;
using NUnit.Framework;

namespace TinyBDDTests.Dsl
{
    [TestFixture]
    public class SimpleScenarioTests
    {
        Scenario builtScenario;
        string output;

        [SetUp]
        public void Setup()
        {
            output = "";
            builtScenario = Scenario.New("Replicate a user to AD", scenario =>
                {
                    scenario.
                        Given("User does not exist", () =>
                            output += "C");

                    scenario.
                        When("ReplicateUser method is called", () =>
                        output += "E").
                    Then("User should be created in AD", () =>
                        output += "O");

                }).End();
        }

        [Test]
        public void Should_return_scenario()
        {
            builtScenario.ShouldNotBeNull();
        }

        [Test]
        public void Title_should_be_set_for_scenario()
        {
            builtScenario.Title.ShouldEqual("Replicate a user to AD");
        }

        [Test]
        public void Should_contain_context()
        {
            builtScenario.Context.ShouldNotBeNull();
            builtScenario.Context.Count.ShouldEqual(1);
            builtScenario.Context[0].Title.ShouldEqual("User does not exist");
        }

        [Test]
        public void Should_contain_an_event()
        {
            builtScenario.Events.ShouldNotBeNull();
            builtScenario.Events.Count().ShouldEqual(1);
            builtScenario.Events.First().Title.ShouldEqual("ReplicateUser method is called");
        }

        [Test]
        public void Should_be_able_to_run_scenario()
        {
            builtScenario.Run();
            output.ShouldEqual("CEO");
        }
    }
}
