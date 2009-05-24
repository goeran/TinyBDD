using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public delegate void Context();

    public delegate void When();

    public class Semantics
    {
        SemanticModel.AAA semanticModel;

        public Semantics(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public GivenSemantics Given(Context context)
        {
            return Given(string.Empty, () => { context(); });
        }

        public GivenSemantics Given(string text)
        {
            return new GivenSemantics(semanticModel);
        }

        public GivenSemantics Given(string text, Action action)
        {
            var givenSemantics = new GivenSemantics(semanticModel);

            semanticModel.Arrange(text, action);

            return givenSemantics;
        }

        public void When(When when)
        {
            When(string.Empty, () => { when(); });
        }

        public void When(string text)
        {
            semanticModel.Act(text, () => { });
        }

        public void When(string text, Action action)
        {
            semanticModel.Act(text, action);
        }

        public ThenSemantics Then(string text, Action action)
        {
            semanticModel.Assert(text, action);

            return new ThenSemantics(semanticModel);
        }

   }
}
