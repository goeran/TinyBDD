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
        public void Context_should_be_reinitialized_for_every_event()
        {
            Scenario.New("Scenario with several events", scenario =>
            {
                Scenario scenarioDomainModel = null;
                List<String> aList = new List<string>();

                scenario.
                    Given("New scenario is created", () =>
                        scenarioDomainModel = new Scenario()).
                    And("one context is added", () =>
                        aList.Add("Context.Setup")).
                    And("two events are added", () =>
                        {
                            scenarioDomainModel.AddEvent("Event 1", () => {});
                            scenarioDomainModel.AddEvent("Event 2", () => {});
                        });
            
                scenario.
                    When("scenario is executed", () =>
                        scenarioDomainModel.Run()).
                    Then("context should be setup for every event", () =>
                        aList.Count.ShouldEqual(1));
            }).Run();
        }

        [Test]
        public void New_object_should_have_this_init_state()
        {
            Scenario.New("New instance of Scenario", scenario =>
            {
                Scenario scenarioDomainModel = null;
                
                scenario.
                    When("a new instance is created", () =>
                        scenarioDomainModel = new Scenario()).
                    Then("the instance should been created", () =>
                        scenarioDomainModel.ShouldNotBeNull()).
                    And("it should have an empty context", () =>
                        scenarioDomainModel.Context.ShouldNotBeNull()).
                    And("it should not contain any events", () =>
                        scenarioDomainModel.Events.ShouldNotBeNull());
            }).Run();
        }

        [Test]
        public void Can_have_several_events()
        {
            Scenario.New("Scenario can have several events", scenario =>
            {
                Scenario scenarioDomainModel = null;

                scenario.
                    Given("a new scenario is created", () =>
                        scenarioDomainModel = new Scenario());
            
                scenario.
                    When("one event is added", () =>
                        scenarioDomainModel.AddEvent("new event", () => {})).
                    Then("it should have one event", () =>
                        scenarioDomainModel.Events.Count().ShouldEqual(1));

                scenario.
                    When("two events is added", () =>
                        {
                            scenarioDomainModel.AddEvent("event 1", () => { });
                            scenarioDomainModel.AddEvent("event 2", () => { });
                        }).
                    Then("it should have two events", () =>
                        scenarioDomainModel.Events.Count().ShouldEqual(2));
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
