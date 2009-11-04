using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using NUnit.Framework;
using TinyBDD.SemanticModel;
using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.Dsl.GivenWhenThen.ScenarioClassDslTests
{
    public class Shared : ScenarioClass
    {
        protected AAAMemento semanticModelState;

        protected Context there_are_changesets_in_sourceControl = () => { };
        protected When Controller_notified_to_refresh = () => { };
        protected Then assure_all_changesets_are_received = () => { };

        protected override void SemanticModelState(AAAMemento semanticModelState)
        {
            this.semanticModelState = semanticModelState;
        }
    }

    [TestFixture]
    public class When_describing_Scenario : Shared
    {
        [SetUp]
        public void Setup()
        {
            Given(there_are_changesets_in_sourceControl).
                And("controller has been created");

            When(Controller_notified_to_refresh);
        }

        [Test]
        public void Assure_all_changesets_are_received()
        {
            Then(() => { }).And("they are ordered by revision", () =>{});
            StartScenario();

            //Asserts that the scenario is built as expected
            semanticModelState.Text.ShouldBe("When describing Scenario");
            semanticModelState.Arranges.Count.ShouldBe(2);
            semanticModelState.Acts.Count.ShouldBe(1);
            semanticModelState.Acts.First().Value.Count.ShouldBe(2);
        }

        [Test]
        public void Assure_changesets_are_added_to_ViewModel()
        {
            Then(() => { });
            StartScenario();

            //Asserts that the scenario is built as expected
            semanticModelState.Text.ShouldBe("When describing Scenario");
            semanticModelState.Arranges.Count.ShouldBe(2);
            semanticModelState.Acts.Count.ShouldBe(1);
            semanticModelState.Acts.First().Value.Count.ShouldBe(1);
        }
    }

    [TestFixture]
    public class When_describing_Scenario_using_test_methods_for_Thens : Shared
    {
        [SetUp]
        public void Setup()
        {
            Given(there_are_changesets_in_sourceControl).
                And("Controller has been created");

            When(Controller_notified_to_refresh);
        }

        [Test]
        public void Assure_repository_is_contacted()
        {
            Then(() => { });
            StartScenario();

            //Asserts that the scenario is built as expected
            semanticModelState.Text.ShouldBe("When describing Scenario using test methods for Thens");
            semanticModelState.Arranges[0].Text.ShouldBe("there are changesets in sourceControl");
            semanticModelState.Arranges[1].Text.ShouldBe("Controller has been created");
            semanticModelState.Acts.First().Key.Text.ShouldBe("Controller notified to refresh");
            semanticModelState.Acts.First().Value.First().Text.ShouldBe("Assure repository is contacted");
        }
    }

    [TestFixture]
    public class When_describing_empty_Scenario : Shared
    {
        [Test]
        public void Test()
        {
            Scenario("");
            Given("there are changesets in sourceControl");
            //Run();
        }
    }
}
