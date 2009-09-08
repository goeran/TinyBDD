using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ScenarioClass<TClass>
    {
        private AAA semanticModel;
        private Semantics scenario;

        public ScenarioClass()
        {
            
        }

        public ScenarioClass(AAA semanticModel)
        {
            this.semanticModel = semanticModel;
            scenario = new Semantics(Activator.CreateInstance(typeof(TClass)), semanticModel);
        }

        public GivenSemantics Given(string text)
        {
            return scenario.Given(text);
        }

        public GivenSemantics Given(string text, Action action)
        {
            return scenario.Given(text, action);
        }

        public GivenSemantics Given(Context context)
        {
            return scenario.Given(context);
        }

        public void When(string text)
        {
            scenario.When(text);
        }

        public void When(string text, Action action)
        {
            scenario.When(text, action);
        }

        public void When(When when)
        {
            scenario.When(when);
        }

        public ThenSemantics Then(string text)
        {
            return scenario.Then(text);
        }

        public ThenSemantics Then(string text, Action action)
        {
            return scenario.Then(text, action);
        }

        public ThenSemantics Then(Then then)
        {
            return scenario.Then(then);
        }
    }
}
