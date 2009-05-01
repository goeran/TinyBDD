using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ThenSemantics
    {
        SemanticModel.AAA semanticModel;

        public ThenSemantics(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public ThenSemantics And(string text, Action action)
        {
            this.semanticModel.Assert(text, action);

            return new ThenSemantics(semanticModel);
        }
    }
}
