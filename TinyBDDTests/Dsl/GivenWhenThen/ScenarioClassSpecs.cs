﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.SemanticModel;
using TinyBDD.Specification.NUnit;
using Assert=NUnit.Framework.Assert;

namespace TinyBDDTests.Dsl.GivenWhenThen.ScenarioClassSpecs
{
    public class Changeset_notification : ScenarioClass
    {
        public static Context there_are_changesets_in_SourceControl = () => { };
        public static When notified_to_refresh = () => { };
        public static Then assure_all_changesets_are_received = () => { };

        public Changeset_notification()
        {
            
        }

        public Changeset_notification(AAAMemento semanticModelState)
            : base(semanticModelState)
        {
            
        }
    }

    public class Shared
    {
        protected AAAMemento semanticModelState;
        protected ScenarioClass scenario;

        protected void SetupContext()
        {
            semanticModelState = new AAAMemento();
            scenario = new Changeset_notification(semanticModelState);
        }
    }

    [TestFixture]
    public class When_desribing_Scenario : Shared
    {
        [SetUp]
        public void Setup()
        {
            SetupContext();
        }

        [Test]
        public void Assure_class_name_is_used_as_text_for_the_scenario()
        {
            semanticModelState.Text.ShouldBe("Changeset notification");
        }

        [Test]
        public void Assure_its_possible_to_set_custom_text_for_Scenario()
        {
            scenario.Scenario("custom text");

            semanticModelState.Text.ShouldBe("custom text");
        }
    }

    [TestFixture]
    public class When_describing_Scenario_Givens : Shared
    {
        [SetUp]
        public void Setup()
        {
            SetupContext();
        }

        [Test]
        public void Assure_Given_add_arrange_to_the_SemanticModel()
        {
            scenario.Given("there are changesets in SourceControl");
            AssertArrange("there are changesets in SourceControl");
            semanticModelState.Arranges.Clear();

            scenario.Given("there are changesets in SourceControl", () => { });
            AssertArrange("there are changesets in SourceControl");
            semanticModelState.Arranges.Clear();

            scenario.Given(Changeset_notification.there_are_changesets_in_SourceControl);
            AssertArrange("there are changesets in SourceControl");
            semanticModelState.Arranges.Clear();
        }

        [Test]
        public void Assure_Given_with_only_text_returns_semantics()
        {
            var semantics = scenario.Given("there are changesets in SourceControl");

            semantics.ShouldBeInstanceOfType<GivenSemantics>();
        }

        [Test]
        public void Assure_Given_with_text_and_code_returns_semantics()
        {
            var semantics = scenario.Given("there are changesets in SourceControl", () => { });

            semantics.ShouldBeInstanceOfType<GivenSemantics>();
        }

        [Test]
        public void Assure_Given_with_Context_returns_semantics()
        {
            var semantics = scenario.Given(Changeset_notification.there_are_changesets_in_SourceControl);

            semantics.ShouldBeInstanceOfType<GivenSemantics>();
        }

        [Test]
        public void Assure_its_possible_to_reuse_Context()
        {
            scenario.Given(There_are_changesets_and_Controller_is_created());

            semanticModelState.Arranges.Count.ShouldBe(2);
        }

        private GivenSemantics There_are_changesets_and_Controller_is_created()
        {
            return scenario.Given("there are changesets in sourceControl").
                And("the controller is created");
        }

        private void AssertArrange(string text)
        {
            semanticModelState.Arranges.ShouldHave(1);
            semanticModelState.Arranges.First().Text.ShouldBe(text);
        }
    }

    [TestFixture]
    public class When_describing_Scenario_Whens : Shared
    {
        [SetUp]
        public void Setup()
        {
            SetupContext();
        }

        [Test]
        public void Assure_When_add_act_to_the_SemanticModel()
        {
            scenario.When("notified to refresh");
            AssertAct("notified to refresh");
            semanticModelState.Acts.Clear();

            scenario.When("notified to refresh", () => { });
            AssertAct("notified to refresh");
            semanticModelState.Acts.Clear();

            scenario.When(Changeset_notification.notified_to_refresh);
            AssertAct("notified to refresh");
            semanticModelState.Acts.Clear();
        }

        private void AssertAct(string text)
        {
            semanticModelState.Acts.Count.ShouldBe(1);
            semanticModelState.Acts.Keys.First().Text.ShouldBe(text);
        }
    }

    public class When_describing_Scenario_Thens : Shared
    {
        [SetUp]
        public void Setup()
        {
            SetupContext();

            //The SemanticModel expect and act, before an
            //assert can take place.
            scenario.When("notified to refresh");
        }

        [Test]
        public void Assure_Then_add_Assert_to_the_SemanticModel()
        {
            scenario.Then("all changesets are received");
            VerifyAssertBeenAdded("all changesets are received");
            semanticModelState.Acts.Clear();

            scenario.When("notified to refresh");
            scenario.Then("all changesets are received", () => { });
            VerifyAssertBeenAdded("all changesets are received");
            semanticModelState.Acts.Clear();

            scenario.When("notified to refresh");
            scenario.Then(Changeset_notification.assure_all_changesets_are_received);
            VerifyAssertBeenAdded("assure all changesets are received");
        }

        [Test]
        public void Assure_Then_with_only_text_returns_semantics()
        {
            var semantics = scenario.Then("assure all changesets are received");

            semantics.ShouldBeInstanceOfType<ThenSemantics>();
        }

        [Test]
        public void Assure_Then_with_text_and_code_returns_semantics()
        {
            var semantics = scenario.Then("assure all changesets are received", () => { });

            semantics.ShouldBeInstanceOfType<ThenSemantics>();
        }

        [Test]
        public void Assure_Then_with_Then_returns_semnatics()
        {
            var semantics = scenario.Then(Changeset_notification.assure_all_changesets_are_received);

            semantics.ShouldBeInstanceOfType<ThenSemantics>();
        }

        [Test]
        public void Assure_its_possible_to_reuse_Thens()
        {
            scenario.Then(Assure_changesets_are_received_and_sorted());

            semanticModelState.Acts.Values.First().Count.ShouldBe(2);
        }

        private ThenSemantics Assure_changesets_are_received_and_sorted()
        {
            return scenario.Then("assure all changesets are received").
                And("they are sorted");
        }

        private void VerifyAssertBeenAdded(string text)
        {
            semanticModelState.Acts.Values.First().ShouldHave(1);
            semanticModelState.Acts.Values.First().First().Text.ShouldBe(text);
        }


    }
}