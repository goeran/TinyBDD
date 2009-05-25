using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public interface IGenerateSpecDocument
    {
        void Generate(SemanticModel.AAAMemento semanticModelState);
    }
}
