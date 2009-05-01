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

        public Scenario()
        {
            semanticModelState = new SemanticModel.AAAMemento();
            semanticModel = new SemanticModel.AAA(semanticModelState);
            semantics = new Semantics(semanticModel);
        }

        public static ScenarioSpecialCase New(string text, Action<Semantics> action)
        {
            var scenario = new Scenario();
            action.Invoke(scenario.semantics);

            return new ScenarioSpecialCase(scenario.semanticModel)
            {
                State = scenario.semanticModelState
            };
        }
    }
}
