using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using SM = TinyBDD.SemanticModel;

namespace TinyBDDTests.Dsl.GivenWhenThen
{
    [TestFixture]
    public class GivenSemanticsTests
    {
        GivenSemantics givenSemantics;
        SM.AAAMemento semanticModelState;
        SM.AAA semanticModel;

        [SetUp]
        public void Setup()
        {
            semanticModelState = new SM.AAAMemento();
            semanticModel = new SM.AAA(semanticModelState);
            givenSemantics = new GivenSemantics(this, semanticModel);
        }

        Context there_are_changesets_in_sourceControl = () => { };

        [Test]
        public void And_should_add_Arrange_to_SemanticModel()
        {
            givenSemantics.And("there are changesets in sourceControl", () => { });

            semanticModelState.Arranges.First().Text.ShouldBe("there are changesets in sourceControl");
        }        

        [Test]
        public void And_should_add_Arrange_to_semanticModel_when_text_is_specified()
        {
            givenSemantics.And("there are changesets in sourceControl");

            semanticModelState.Arranges.First().Text.ShouldBe("there are changesets in sourceControl");
        }

        [Test]
        public void And_should_add_Arrange_to_semanticModel_when_context_is_specified()
        {
            givenSemantics.And(there_are_changesets_in_sourceControl);

            semanticModelState.Arranges.First().Text.ShouldBe("there are changesets in sourceControl");
        }
    }
}
