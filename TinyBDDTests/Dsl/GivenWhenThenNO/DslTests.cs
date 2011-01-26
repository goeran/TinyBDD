using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThenNO;
using TinyBDDTests.Extensions;

namespace TinyBDDTests.Dsl.GivenWhenThenNO
{
    [TestFixture]
    public class DslTests
    {
        private AAAMemento semanticModelState;

        [SetUp]
        public void Setup()
        {
            semanticModelState = Scenario.Nytt("Timelønnte i OM bedrifter skal reguleres en gang i kvartalet", this, scenario =>
            {
                scenario.Gitt("bedriften har ansatte knyttet til en OM avtale")
                        .Og("det er nytt kvartal");

                scenario.Når("kjører bestandsbehandling");
                

                scenario.Så("skal vi summere opptjent lønn i forrige kvartal")
                        .Og("timelønnte skal reguleres");

            });
        }

        [Test]
        public void Semantisk_Model_Skal_Ha_Arrange()
        {
            semanticModelState.Arranges.Count.ShouldBe(2);
        }

        [Test]
        public void Arrange_Skal_Ha_En_Tekstlig_Beskrivelse()
        {
            semanticModelState.Arranges.First().Text.ShouldBe("bedriften har ansatte knyttet til en OM avtale");
        }

        [Test]
        public void Semantisk_Model_Skal_Ha_En_Act()
        {
            semanticModelState.Acts.Count.ShouldBe(1);
        }

        [Test]
        public void Act_Skal_Ha_En_Tekstlig_Beskrivelse()
        {
            semanticModelState.Acts.First().Key.Text.ShouldBe("kjører bestandsbehandling");
        }

        [Test]
        public void Semantisk_Model_Skal_Ha_Assert()
        {
            semanticModelState.Acts.First().Value.Count.ShouldBe(2);
        }

        [Test]
        public void Assert_Skal_Ha_En_Tekstlig_Beskrivelse()
        {
            semanticModelState.Acts.First().Value.First().Text.ShouldBe("skal vi summere opptjent lønn i forrige kvartal");
        }                     
    }
}
