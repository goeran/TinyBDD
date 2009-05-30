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
    public class ThenSemanticsTests
    {
        ThenSemantics thenSemantics;
        SM.AAAMemento semanticModelState;
        SM.AAA semanticModel;

        [SetUp]
        public void Setup()
        {
            semanticModelState = new SM.AAAMemento();
            semanticModel = new SM.AAA(semanticModelState);
            semanticModel.Act("user checkout", () => { });

            thenSemantics = new ThenSemantics(this, semanticModel);
        }

        Then latest_changeset_should_be_downloaded = () =>
        {
        };

        [Test]
        public void And_should_add_Assert_to_the_semanticModel()
        {
            thenSemantics.And("latest changeset should be downloaded", () => { });

            semanticModelState.Acts.Values.First().First().Text.ShouldBe("latest changeset should be downloaded");
        }

        [Test]
        public void And_should_add_Assert_to_the_semanticModel_when_Text_is_specified()
        {
            thenSemantics.And("latest changeset should be downloaded");

            semanticModelState.Acts.Values.First().First().Text.ShouldBe("latest changeset should be downloaded");
        }

        [Test]
        public void And_should_add_Assert_to_the_semanticModel_when_Then_is_specified()
        {
            thenSemantics.And(latest_changeset_should_be_downloaded);

            semanticModelState.Acts.Values.First().First().Text.ShouldBe("latest changeset should be downloaded");
        }
    }
}
