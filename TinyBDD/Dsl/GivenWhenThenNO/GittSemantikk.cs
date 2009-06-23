using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyBDD.Dsl.GivenWhenThenNO
{
    public class GittSemantikk
    {
        private AAA semantiskModell;
        private TestMetadataParser metadataParser;
        private object test;

        public GittSemantikk(AAA semantiskModell, object test)
        {
            this.test = test;
            this.semantiskModell = semantiskModell;
            this.metadataParser = new TestMetadataParser(test);
        }

        public GittSemantikk Og(Gitt kode)
        {
            return Og(metadataParser.TranslateToText(kode), () => { kode(); });
        }

        public GittSemantikk Og(string tekstligBeskrivelse)
        {
            return Og(tekstligBeskrivelse, () => { });
        }

        public GittSemantikk Og(string tekstligBeskrivelse, Action kode)
        {
            semantiskModell.Arrange(tekstligBeskrivelse, kode);
            return new GittSemantikk(semantiskModell, test);
        }
    }
}
