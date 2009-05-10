using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class GivenSemantics
    {
        SemanticModel.AAA semanticModel;

        public GivenSemantics(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public GivenSemantics And(string text)
        {
            return new GivenSemantics(semanticModel);
        }

        public GivenSemantics And(string text, Action action)
        {
            semanticModel.Arrange(text, action);

            return new GivenSemantics(semanticModel);
        }
    }
}
