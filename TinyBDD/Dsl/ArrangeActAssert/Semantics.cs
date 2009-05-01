using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.ArrangeActAssert
{
    public class Semantics
    {
        SemanticModel.AAA semanticModel;

        public Semantics(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public void Arrange(Action action)
        {
            semanticModel.Arrange(string.Empty, action);
        }

        public void Act(Action action)
        {
            semanticModel.Act(string.Empty, action);
        }

        public void Assert(Action action)
        {
            semanticModel.Assert(string.Empty, action);
        }
    }
}
