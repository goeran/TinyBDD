using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.ArrangeActAssert
{
    public class AAASpecialCase
    {
        SemanticModel.AAA semanticModel;

        public SemanticModel.AAAMemento State { get; set; }
        
        public AAASpecialCase(SemanticModel.AAA semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public void Execute()
        {
            semanticModel.Execute();
        }
    }
}
