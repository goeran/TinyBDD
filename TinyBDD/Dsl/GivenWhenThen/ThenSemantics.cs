using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ThenSemantics
    {
        SemanticModel.AAA semanticModel;
        Object test;
        TestMetadataParser metadataParser;

        public ThenSemantics(Object test, SemanticModel.AAA semanticModel)
        {
            this.test = test;
            this.semanticModel = semanticModel;
            metadataParser = new TestMetadataParser(test);
        }

        public ThenSemantics And(Then then)
        {
            return And(metadataParser.TranslateToText(then), () => { then(); });
        }

        public ThenSemantics And(string text)
        {
            return And(text, () => { });
        }

        public ThenSemantics And(string text, Action action)
        {
            this.semanticModel.Assert(text, action);

            return new ThenSemantics(test, semanticModel);
        }
    }
}
