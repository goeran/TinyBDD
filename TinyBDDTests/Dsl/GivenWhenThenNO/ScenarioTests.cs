using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThenNO;
using TinyBDDTests.Extensions;

namespace TinyBDDTests.Dsl.GivenWhenThenNO
{
    [TestFixture]
    public class ScenarioTests
    {
        [Test]
        public void Skal_kunne_spesifisere_og_starte_nytt_scenario()
        {
            var harKjørt = false;
            Scenario.Start("Timelønnte i OM bedrifter skal reguleres en gang i kvartalet", this, scenario =>
            {
                scenario.Gitt("bedriften har ansatte knyttet til en OM avtale", () => harKjørt = true)
                        .Og("det er nytt kvartal");

                scenario.Når("kunden kjører bestandsbehandling");


                scenario.Så("skal vi summere opptjent lønn i forrige kvartal")
                        .Og("timelønnte skal reguleres");

            });

            harKjørt.ShouldBe(true);
        }
        
    }
}
