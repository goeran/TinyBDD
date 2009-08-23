using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class Behaviour<TClass>
    {
        private AAA semanticModel;
        private Semantics scenario;

        public Behaviour()
        {
            
        }

        public Behaviour(AAA semanticModel)
        {
            this.semanticModel = semanticModel;
            scenario = new Semantics(Activator.CreateInstance(typeof(TClass)), semanticModel);
        }

        public void Given(string text)
        {
            scenario.Given(text);
        }

        public void Given(string text, Action action)
        {
            scenario.Given(text, action);
        }

        public void Given(Context context)
        {
            scenario.Given(context);
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

        public void Then(string text)
        {
            scenario.Then(text);
        }

        public void Then(string text, Action action)
        {
            scenario.Then(text, action);
        }
    }
}
