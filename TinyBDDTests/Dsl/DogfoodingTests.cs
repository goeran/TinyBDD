using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD;

namespace TinyBDDTests.Dsl
{
    [TestFixture]
    public class DogfoodingTests
    {
        [Test]
        public void Run()
        {
            string output = "";
            Scenario.New("Developer setup a simple scenario", scenario =>
                {
                    output = "";
                    scenario.
                        Given("A simple scenario is described", () =>
                            output += "C");

                    scenario.
                        When("It's executed inside a test runner", () =>
                            output += "E").
                        Then("The developer should be able to do assertions on the outcome", () =>
                        {
                            output += "O";
                            output.ShouldEqual("CEO");
                        });

                }).Run();

            Scenario.New("Developer setup a complex scenario", scenario =>
                {
                    output = "";
            
                    scenario.
                        Given("A complex scenario is described", () =>
                            output += "C").
                        And("with several items in context", () =>
                            output += "C");

                    scenario.
                        When("It's executed inside a test runner", () =>
                            output += "E").
                        Then("The developer should be able to do assertions inside the outcome", () =>
                        {
                            output += "O";
                            output.ShouldEqual("CCEO");
                        }).
                        And("Yet another assertion is added inside the outcome", () =>
                        {
                            output += "O";
                            output.ShouldEqual("CCEOO");
                        });
                }).Run();

            
        }
    }
}
