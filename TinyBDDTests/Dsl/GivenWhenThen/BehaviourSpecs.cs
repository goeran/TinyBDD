using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.SemanticModel;
using TinyBDD.Specification.NUnit;
using Assert=NUnit.Framework.Assert;

namespace TinyBDDTests.Dsl.GivenWhenThen.BehaviourSpecs
{
    public class Changeset_notification : Behaviour<Changeset_notification>
    {
        public static Context there_are_changesets_in_SourceControl = () => { };
        public static When notified_to_refresh = () => { };

        public Changeset_notification()
        {
            
        }

        public Changeset_notification(AAA semanticModel)
            : base(semanticModel)
        {
            
        }
    }

    [TestFixture]
    public class When_describing_scenario
    {
        private AAA semanticModel;
        private AAAMemento semanticModelState;
        private Behaviour<Changeset_notification> scenario;

        [SetUp]
        public void Setup()
        {
            semanticModelState = new AAAMemento();
            semanticModel = new AAA(semanticModelState);
            scenario = new Changeset_notification(semanticModel);
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

        private void AssertArrange(string text)
        {
            semanticModelState.Arranges.ShouldHave(1);
            semanticModelState.Arranges.First().Text.ShouldBe(text);
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

        [Test]
        public void Assure_Then_add_Assert_to_the_SemanticModel()
        {
            //The SemanticModel expect and act, before an
            //assert can take place.
            scenario.When("notified to refresh");

            scenario.Then("all changesets are received");
            VerifyAssertBeenAdded("all changesets are received");
            semanticModelState.Acts.Clear();

            scenario.When("notified to refresh");
            scenario.Then("all changesets are received", () => { });
            VerifyAssertBeenAdded("all changesets are received");
            semanticModelState.Acts.Clear();
        }

        private void VerifyAssertBeenAdded(string text)
        {
            semanticModelState.Acts.Values.First().ShouldHave(1);
            semanticModelState.Acts.Values.First().First().Text.ShouldBe(text);
        }

    }
}
