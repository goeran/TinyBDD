using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ScenarioSpecialCase
    {
        SemanticModel.AAA semanticModel;

        public SemanticModel.AAAMemento State { get; set; }

        public ScenarioSpecialCase(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public void Execute()
        {
            semanticModel.Execute();
        }
    }
}
