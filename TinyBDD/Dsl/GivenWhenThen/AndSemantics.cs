using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class AndSemantics
    {
        SemanticModel.AAA semanticModel;
        TestMetadataParser metadataParser;
        Object test;

        public AndSemantics(Object test, SemanticModel.AAA semanticModel)
        {
            this.test = test;
            this.semanticModel = semanticModel;
            metadataParser = new TestMetadataParser(test);
        }

    }
}
