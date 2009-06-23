using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class AAAMemento
    {
        public string Text { get; set; }
        public List<Arrange> Arranges { get; set; }
        public Dictionary<Act, List<Assert>> Acts { get; set; }

        public AAAMemento()
        {
            Arranges = new List<Arrange>();
            Acts = new Dictionary<Act, List<Assert>>();
        }
    }
}
