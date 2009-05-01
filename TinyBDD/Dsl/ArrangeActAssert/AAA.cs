using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.ArrangeActAssert
{
    public class AAA
    {
        SemanticModel.AAA semanticModel;
        SemanticModel.AAAMemento semanticModelMemento;
        Semantics semantics;

        public AAA()
        {
            semanticModelMemento = new SemanticModel.AAAMemento();
            semanticModel = new SemanticModel.AAA(semanticModelMemento);
            semantics = new Semantics(semanticModel);
        }

        public static AAASpecialCase New(Action<Semantics> action)
        {
            var aaa = new AAA();
            action.Invoke(aaa.semantics);

            return new AAASpecialCase(aaa.semanticModel)
            {
                State = aaa.semanticModelMemento,
            };
        }

    }
}
