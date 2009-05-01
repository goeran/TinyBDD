using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class AAAMemento
    {
        public List<Arrange> Arranges { get; set; }
        public List<Act> Acts { get; set; }
        public List<Assert> Asserts { get; set; }

        public AAAMemento()
        {
            Arranges = new List<Arrange>();
            Acts = new List<Act>();
            Asserts = new List<Assert>();
        }
    }
}
