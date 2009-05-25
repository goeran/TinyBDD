using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyBDD.Dsl.GivenWhenThen;
using SM = TinyBDD.SemanticModel;

namespace TinyBDDTests.Dsl.GivenWhenThen
{
    [TestFixture]
    public class SemanticsTests
    {
        SM.AAAMemento semanticModelState;
        SM.AAA semanticModel;
        Semantics semantics;
        string there_are_changesets_in_SourceControl;

        [SetUp]
        public void Setup()
        {
            semanticModelState = new SM.AAAMemento();
            semanticModel = new SM.AAA(semanticModelState);
            semantics = new Semantics(this, semanticModel);
            there_are_changesets_in_SourceControl = "there are changesets in SourceControl";
        }

        [Test]
        public void Given_should_add_arrange_to_the_semanticModel_when_text_is_specified()
        {
            semantics.Given(there_are_changesets_in_SourceControl);

            VerifyThatArrangeHaveBeenAddedInTheSemanticModel(there_are_changesets_in_SourceControl);

        }

        private void VerifyThatArrangeHaveBeenAddedInTheSemanticModel(string text)
        {
            semanticModelState.Arranges.ShouldHave(1);
            semanticModelState.Arranges.First().Title.ShouldBe(text);
            semanticModelState.Arranges.First().Action.ShouldNotBeNull();
        }

        Context user_exist_in_userdb = () =>
        {
        };

        When user_is_deleted = () =>
        {
        };

        [Test]
        public void Given_should_add_arrange_to_the_semanticModel_when_Context_is_specificed()
        {
            semantics.Given(user_exist_in_userdb);

            VerifyThatArrangeHaveBeenAddedInTheSemanticModel("user exist in userdb");
        }

        [Test]
        public void Given_should_add_arrange_to_the_semanticModel()
        {
            semantics.Given(there_are_changesets_in_SourceControl, () => { });

            VerifyThatArrangeHaveBeenAddedInTheSemanticModel(there_are_changesets_in_SourceControl);
        }

        [Test]
        public void When_should_add_act_to_the_semanticModel_when_text_is_specified()
        {
            semantics.When("user is deleted");

            VerifyThatActHaveBeenAddedInTheSemanticModel("user is deleted");
        }

        private void VerifyThatActHaveBeenAddedInTheSemanticModel(string title)
        {
            semanticModelState.Acts.ShouldHave(1);
            semanticModelState.Acts.First().Title.ShouldBe(title);
            semanticModelState.Acts.First().Action.ShouldNotBeNull();
        }

        [Test]
        public void When_should_add_act_to_the_semanticModel_when_When_is_specified()
        {
            semantics.When(user_is_deleted);

            VerifyThatActHaveBeenAddedInTheSemanticModel("user is deleted");
        }

        [Test]
        public void When_should_add_act_to_the_semanticModel()
        {
            semantics.When("user is deleted", () => { });

            VerifyThatActHaveBeenAddedInTheSemanticModel("user is deleted");
        }

        [Test]
        public void Then_should_add_assert_to_the_semanticModel()
        {
            semantics.When(user_is_deleted);
            semantics.Then("user should be deleted from the database", () => { });

            semanticModelState.Asserts.ShouldHave(1);
            semanticModelState.Asserts.First().Title.ShouldBe("user should be deleted from the database");
            semanticModelState.Asserts.First().Action.ShouldNotBeNull();
        }

        [Test]
        public void Reused_context_should_be_translated_to_a_title_if_its_a_private_field_in_test_class()
        {
            semantics.Given(user_exist_in_userdb);
            semanticModelState.Arranges.ShouldHave(1);
            semanticModelState.Arranges.First().Title.ShouldBe("user exist in userdb");
        }

        [Test]
        public void Reused_context_should_not_be_translated_to_a_title_if_its_not_a_private_field_in_test_class()
        {
            Context user_does_not_exist_in_userdb = () => { };

            semantics.Given(user_does_not_exist_in_userdb);

            semanticModelState.Arranges.ShouldHave(1);
            semanticModelState.Arranges.First().Title.ShouldBe(string.Empty);
        }

        [Test]
        public void Reused_event_should_be_translated_to_a_title_if_its_a_private_field_in_test_class()
        {
            semantics.Given(user_exist_in_userdb);

            semantics.When(user_is_deleted);

            semanticModelState.Acts.ShouldHave(1);
            semanticModelState.Acts.First().Title.ShouldBe("user is deleted");
        }

        [Test]
        public void Reused_event_should_not_be_translated_to_a_title_if_its_not_a_private_field_in_test_class()
        {
            When user_is_created = () => { };

            semantics.When(user_is_created);

            semanticModelState.Acts.ShouldHave(1);
            semanticModelState.Acts.First().Title.ShouldBe(string.Empty);

        }
    }
}
