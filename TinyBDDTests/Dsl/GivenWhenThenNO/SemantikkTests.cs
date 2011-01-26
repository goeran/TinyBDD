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
    public class Delt
    {
        protected Gitt min_beskrivelse = () => { };
        protected Når noe_skjer = () => { };
        protected Så har_dette_skjedd = () => { };

        protected AAAMemento semantiskModelTilstand;
        protected AAA semantiskModel;

        protected void SettOppTest()
        {
            semantiskModelTilstand = new AAAMemento();
            semantiskModel = new AAA(semantiskModelTilstand);
        }
    }

    [TestFixture]
    public class SemantikkTests : Delt
    {
        Semantikk semantikk;

        [SetUp]
        public void SettOpp()
        {
            SettOppTest();
            semantikk = new Semantikk(semantiskModel, this);
        }

        [Test]
        public void Skal_Kunne_Spesifisere_Kode_For_Arrange()
        {
            Action kode = () => { };
            semantikk.Gitt("min beskrivelse", kode);

            semantiskModelTilstand.Arranges.First().Action.ShouldNotBeNull();
            semantiskModelTilstand.Arranges.First().Action.ShouldBe(kode);
        }

        [Test]
        public void Skal_Kunne_Spesifisere_Gjenbrukt_Kode_For_Arrange()
        {
            semantikk.Gitt(min_beskrivelse);
            semantiskModelTilstand.Arranges.First().Text.ShouldBe("min beskrivelse");
        }

        [Test]
        public void Gitt_skal_returnere_egen_semantikk()
        {
            semantikk.Gitt("min beskrivelse").GetType().ShouldBe(typeof(GittSemantikk));  
        }
        
        [Test]
        public void Skal_Kunne_Spesifisere_Kode_For_Act()
        {
            semantikk.Når("noe skjer...", () => { });

            semantiskModelTilstand.Acts.First().Key.Action.ShouldNotBeNull();
        }

        [Test]
        public void Skal_Kunne_Spesifisere_Gjenbrukt_Kode_For_Act()
        {
            semantikk.Når(noe_skjer);
            semantiskModelTilstand.Acts.First().Key.Text.ShouldBe("noe skjer");
        }

        [Test]
        public void Skal_Kunne_Spesifisere_Kode_For_Assert()
        {
            Action kode = () => { };
            semantikk.Når("noe skjer...");
            semantikk.Så("har følgnede skjedd:", kode);

            semantiskModelTilstand.Acts.First().Value.First().Action.ShouldNotBeNull();
            semantiskModelTilstand.Acts.First().Value.First().Action.ShouldBe(kode);
        }

        [Test]
        public void Skal_Kunne_Spesifisere_Gjenbrukt_Kode_For_Assert()
        {
            semantikk.Når(noe_skjer);
            semantikk.Så(har_dette_skjedd);

            semantiskModelTilstand.Acts.First().Value.First().Text.ShouldBe("har dette skjedd");            
        }

        [Test]
        public void Så_skal_returnere_egen_semantikk()
        {
            semantikk.Når("noe skjer...");
            semantikk.Så("har følgende skjedd:").GetType().ShouldBe(typeof(SåSemantikk));
        }
        
    }

    [TestFixture]
    public class GittSemantikkTest : Delt
    {
        GittSemantikk semantikk;

        [SetUp]
        public void SettOpp()
        {
            SettOppTest();
            semantikk = new GittSemantikk(semantiskModel, this);
        }       

        [Test]
        public void Skal_kunne_legge_til_Arrange()
        {
            semantikk.Og("enda en beskrivelse");

            semantiskModelTilstand.Arranges.Count.ShouldBe(1);
        }

        [Test]
        public void Arrange_skal_ha_tekstlig_beskrivelse()
        {
            semantikk.Og("enda en beskrivelse");

            semantiskModelTilstand.Arranges.First().Text.ShouldBe("enda en beskrivelse");
        }

        [Test]
        public void Skal_kunne_spesifisere_kode_for_Arrange()
        {
            Action kode = () => { };
            semantikk.Og("enda en beskrivelse", kode);

            semantiskModelTilstand.Arranges.First().Action.ShouldNotBeNull();
            semantiskModelTilstand.Arranges.First().Action.ShouldBe(kode);
        }

        [Test]
        public void Skal_kunne_spesifisere_gjenbrukt_kode_for_Arrange()
        {
            semantikk.Og(min_beskrivelse);

            semantiskModelTilstand.Arranges.First().Action.ShouldNotBeNull();
            semantiskModelTilstand.Arranges.First().Text.ShouldBe("min beskrivelse");
        }
        
    }

    [TestFixture]
    public class SåSemantikkTest : Delt
    {
        SåSemantikk semantikk;

        [SetUp]
        public void SettOpp()
        {
            SettOppTest();

            semantikk = new SåSemantikk(semantiskModel, this);

            semantiskModel.Act("noe skjer", () => { });
        }        


        [Test]
        public void Skal_kunne_legge_til_Assert()
        {
            semantikk.Og("noe mer har skjedd");

            semantiskModelTilstand.Acts.First().Value.Count.ShouldBe(1);
        }

        [Test]
        public void Assert_skal_ha_tekstlig_beskrivelse()
        {
            semantikk.Og("noe mer har skjedd");

            semantiskModelTilstand.Acts.First().Value.First().Text.ShouldBe("noe mer har skjedd");
        }
        
        [Test]
        public void Skal_kunne_spesifisere_kode_for_Assert()
        {
            Action kode = () => { };
            semantikk.Og("noe mer har skjedd", kode);

            semantiskModelTilstand.Acts.First().Value.First().Action.ShouldNotBeNull();
            semantiskModelTilstand.Acts.First().Value.First().Action.ShouldBe(kode);
        }

        [Test]
        public void Skal_kunne_spesifisere_gjenbrukt_kode_for_assert()
        {
            semantikk.Og(har_dette_skjedd);

            semantiskModelTilstand.Acts.First().Value.First().Text.ShouldBe("har dette skjedd");
        }        
    }
        
        
}
