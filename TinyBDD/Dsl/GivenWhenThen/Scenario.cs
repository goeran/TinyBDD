using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class Scenario
    {
        SemanticModel.AAA semanticModel;
        SemanticModel.AAAMemento semanticModelState;
        Semantics semantics;
        Object test;

        public Scenario(Object test)
        {
            this.test = test;
            semanticModelState = new SemanticModel.AAAMemento();
            semanticModel = new SemanticModel.AAA(semanticModelState);
            semantics = new Semantics(test, semanticModel);
        }

        public static ScenarioSpecialCase New(Object test, Action<Semantics> action)
        {
            return New(test, string.Empty, action);
        }

        public static ScenarioSpecialCase New(Object test, string text, Action<Semantics> action)
        {
            var scenario = new Scenario(test);
            action.Invoke(scenario.semantics);

            return new ScenarioSpecialCase(scenario.semanticModel)
            {
                State = scenario.semanticModelState
            };
        }
    }
}
