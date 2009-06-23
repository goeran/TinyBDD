using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyBDD.Dsl.GivenWhenThenNO
{
    public delegate void Gitt();
    public delegate void Når();
    public delegate void Så();

    public class Semantikk
    {
        private AAA semantiskModel;
        private TestMetadataParser metadataParser;
        private object test;

        public Semantikk(AAA semantiskModel, object test)
        {
            this.test = test;
            this.semantiskModel = semantiskModel;
            this.metadataParser = new TestMetadataParser(test);
        }

        public GittSemantikk Gitt(Gitt aksjon)
        {
            return Gitt(metadataParser.TranslateToText(aksjon), () => { aksjon(); });
        }

        public GittSemantikk Gitt(string tekstligBeskrivelse)
        {
            return Gitt(tekstligBeskrivelse, () => { });
        }

        public GittSemantikk Gitt(string tekstligBeskrivelse, Action aksjon)
        {
            semantiskModel.Arrange(tekstligBeskrivelse, aksjon);

            return new GittSemantikk(semantiskModel, test);
        }

        public void Når(Når aksjon)
        {
            Når(metadataParser.TranslateToText(aksjon), () => { aksjon(); });
        }

        public void Når(string tekstligBeskrivelse)
        {
            Når(tekstligBeskrivelse, () => { });
        }

        public void Når(string tekstligBeskrivelse, Action aksjon)
        {
            semantiskModel.Act(tekstligBeskrivelse, aksjon);
        }

        public SåSemantikk Så(Så aksjon)
        {
            return Så(metadataParser.TranslateToText(aksjon), () => { aksjon(); });
        }

        public SåSemantikk Så(string tekstligBeskrivelse)
        {
            return Så(tekstligBeskrivelse, () => { });
        }

        public SåSemantikk Så(string tekstligBeskrivelse, Action aksjon)
        {
            semantiskModel.Assert(tekstligBeskrivelse, aksjon);

            return new SåSemantikk(semantiskModel, test);
        }
    }
}
