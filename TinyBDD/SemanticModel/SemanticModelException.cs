using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class SemanticModelException : Exception
    {
        public SemanticModelException(string message) :
            base(message)
        {
        }
    }
}
