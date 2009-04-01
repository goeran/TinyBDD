using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD;

namespace TinyBDDTests.Dsl
{
    [TestFixture]
    public class NewSyntax
    {
        private Scenario builtScenario;

        [SetUp]
        public void Setup()
        {
            builtScenario = Scenario.New("Account is in credit", scenario =>
                {
                    var x = string.Empty;
                    scenario.
                        Given("the account is in credit", () =>
                            x += "C");

                    scenario.
                        When("when the customer requests cash", () =>
                            x += "E").
                        Then("then ensure the account is debited", () =>
                            {
                                x += "O";
                                x.ShouldEqual("CEO");
                            }); ;

                }).End();
        }

        [Test]
        public void Should_contain_an_title()
        {
            builtScenario.Title.ShouldEqual("Account is in credit");
        }

        [Test]
        public void Should_contain_context()
        {
            builtScenario.Context.Count.ShouldEqual(1);
        }

        [Test]
        public void Should_contain_an_event()
        {
            builtScenario.Events.ShouldNotBeNull();
        }

    }
}
