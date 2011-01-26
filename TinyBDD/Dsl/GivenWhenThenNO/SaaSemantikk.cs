using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyBDD.Dsl.GivenWhenThenNO
{
    public class SåSemantikk
    {
        AAA semantiskModell;
        TestMetadataParser metadataParser;
        object test;

        public SåSemantikk(AAA semantiskModell, object test)
        {
            this.test = test;
            this.semantiskModell = semantiskModell;
            this.metadataParser = new TestMetadataParser(test);
        }

        public SåSemantikk Og(Så kode)
        {
            return Og(metadataParser.TranslateToText(kode), () => { kode(); });
        }

        public SåSemantikk Og(string tekstligBeskrivelse)
        {
            return Og(tekstligBeskrivelse, () => { });
        }

        public SåSemantikk Og(string tekstligBeskrivelse, Action kode)
        {
            semantiskModell.Assert(tekstligBeskrivelse, kode);

            return new SåSemantikk(semantiskModell, test);
        }
    }
}
