using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class Semantics
    {
        SemanticModel.AAA semanticModel;

        public Semantics(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public void Given(string text, Action action)
        {
            semanticModel.Arrange(text, action);
        }

        public void When(string text, Action action)
        {
            semanticModel.Act(text, action);
        }

        public void Then(string text, Action action)
        {
            semanticModel.Assert(text, action);
        }
    }
}
