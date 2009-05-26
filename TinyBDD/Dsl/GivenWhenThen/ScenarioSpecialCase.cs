using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ScenarioSpecialCase
    {
        SemanticModel.AAA semanticModel;
        public SemanticModel.AAAMemento State { get; private set; }
        TextSpecGenerator specDocumentGenerator;
        string scenarioText;

        public ScenarioSpecialCase(string scenarioText, SemanticModel.AAA semanticModel, SemanticModel.AAAMemento state)
        {
            this.scenarioText = scenarioText;
            this.semanticModel = semanticModel;
            State = state;
            specDocumentGenerator = new TextSpecGenerator();
        }

        public void Execute()
        {
            specDocumentGenerator.Generate(scenarioText, State);
            Console.WriteLine(specDocumentGenerator.Output);
            semanticModel.Execute();
        }
    }
}
