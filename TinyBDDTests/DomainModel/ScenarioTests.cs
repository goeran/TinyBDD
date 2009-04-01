using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD;
using TinyBDD.Services;
using Moq;

namespace TinyBDDTests.DomainModel
{
    [TestFixture]
    public class ScenarioTests
    {
        [Test]
        public void Context_should_be_setup_for_every_event()
        {
            Scenario.New("Scenario with several events", scenario =>
            {
                int contextSetupCounter = 0;
                Scenario sc = null;

                scenario.
                    Given("New scenario is created", () =>
                        sc = new Scenario()).
                    And("one context is added", () =>
                        contextSetupCounter++).
                    And("two events are added", () =>
                        {
                            sc.AddEvent("Event 1", () => {});
                            sc.AddEvent("Event 2", () => {});
                        });
            
                scenario.
                    When("scenario is executed", () =>
                        sc.Run()).
                    Then("context should be setup for every event", () =>
                        contextSetupCounter.ShouldEqual(1));
            }).Run();
        
        }

        [Test]
        public void Should_have_init_state()
        {
            Scenario.New("New scenario", scenario =>
            {
                Scenario sc = null;
                scenario.
                    Given("that a Scenario is not created", () =>
                        sc = null);

                scenario.
                    When("a new instance is created", () =>
                        sc = new Scenario()).
                    Then("it should have a scope", () =>
                        sc.ShouldNotBeNull()).
                    And("it should have a context", () =>
                        sc.Context.ShouldNotBeNull());
            }).Run();
        }

        [Test]
        public void Can_have_several_events()
        {
            Scenario.New("Scenario can have several events", scenario =>
            {
                Scenario sc = null;

                scenario.
                    Given("a new scenario is created", () =>
                        sc = new Scenario());
            
                scenario.
                    When("one event is added", () =>
                        sc.AddEvent("new event", () => {})).
                    Then("it should have one event", () =>
                        sc.Events.ShouldNotBeNull());

                scenario.
                    When("two events is added", () =>
                        {
                            sc.AddEvent("event 1", () => { });
                            sc.AddEvent("event 2", () => { });
                        }).
                    Then("it should have two events", () =>
                        sc.Events.ShouldNotBeNull());
            }).Run();
        
        }

        [Test]
        public void Should_log_simple_scenario()
        {
            Scenario.New("Logging", scenario =>
                {
                    Scenario sc = null;
                    Mock<ILog> loggerMock = null;

                    scenario.
                        Given("a scenario is created", () =>
                        {
                            loggerMock = new Mock<ILog>();
                            sc = CreateScenario(loggerMock);
                            loggerMock.Setup(l => l.Text(It.IsAny<string>()));
                        });

                    scenario.
                        When("it's executed", () =>
                            sc.Run()).
                        Then("it should call logger", () =>
                        {
                            loggerMock.Verify(l => l.Text("Scenario: A scenario to test"));
                            loggerMock.Verify(l => l.Text("Given a scenario is created"));
                            loggerMock.Verify(l => l.Text("When it's executed"));
                            loggerMock.Verify(l => l.Text("Then it should call logger during execution"));
                        });
                }).Run();
        }

        [Test]
        public void Should_log_complex_scenario()
        {
            Scenario.New("Complex scenario logging", scenario =>
            {
                Scenario sc = null;
                Mock<ILog> loggerMock = null;

                scenario.
                    Given("a complex scenario is created", () =>
                    {
                        loggerMock = new Mock<ILog>();
                        sc = CreateScenario(loggerMock);
                        sc.AppendContext("a logger is injected", null);
                        loggerMock.Setup(l => l.Text(It.IsAny<string>()));
                    });

                scenario.
                        When("the scenario is executed", () =>
                            sc.Run()).
                        Then("the scenario should call the logger object", () =>
                        {
                            loggerMock.Verify(l => l.Text("Scenario: A scenario to test"));
                            loggerMock.Verify(l => l.Text("Given a scenario is created"));
                            loggerMock.Verify(l => l.Text("And a logger is injected"));
                            loggerMock.Verify(l => l.Text("When it's executed"));
                            loggerMock.Verify(l => l.Text("Then it should call logger during execution"));
                        });
                }).Run();
        }

        private static Scenario CreateScenario(Mock<ILog> loggerMock)
        {
            Scenario scenario = null;
            if (loggerMock != null)
                scenario = new Scenario(loggerMock.Object);
            else
                scenario = new Scenario();

            scenario.Title = "A scenario to test";
            scenario.AppendContext("a scenario is created", null);
            var anEvent = scenario.AddEvent("it's executed", null);
            anEvent.AddOutcome("it should call logger during execution", null);
            return scenario;
        }
    }
}
