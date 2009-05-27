using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class GivenSemantics
    {
        SemanticModel.AAA semanticModel;
        TestMetadataParser metadataParser;
        Object test;

        public GivenSemantics(Object test, SemanticModel.AAA semanticModel)
        {
            this.test = test;
            this.semanticModel = semanticModel;
            metadataParser = new TestMetadataParser(test);
        }

        public GivenSemantics And(Context context)
        {
            return And(metadataParser.TranslateToTitle(context), () => { context(); });
        }

        public GivenSemantics And(string text)
        {
            return And(text, () => { });
        }

        public GivenSemantics And(string text, Action action)
        {
            semanticModel.Arrange(text, action);

            return new GivenSemantics(test, semanticModel);
        }
    }
}
