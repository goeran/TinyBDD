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
            var metadataParser = new TestMetadataParser(test);
            return New(test, metadataParser.TranslateTestClassNameToText(), action);
        }

        public static ScenarioSpecialCase New(Object test, string text, Action<Semantics> action)
        {
            var scenario = new Scenario(test);
            scenario.semanticModel.Text(text);

            action.Invoke(scenario.semantics);

            return new ScenarioSpecialCase(scenario.semanticModel, scenario.semanticModelState);
        }

        public static ScenarioSpecialCase StartNew(Object test, Action<Semantics> action)
        {
            var scenario = New(test, action);
            scenario.Execute();
            return scenario;
        }

        public static ScenarioSpecialCase StartNew(Object test, string text, Action<Semantics> action)
        {
            var scenario = New(test, text, action);
            scenario.Execute();
            return scenario;
        }
    }
}
