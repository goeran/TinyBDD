using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class AAAMemento
    {
        public Arrange Arrange { get; set; }
        public List<Act> Acts { get; set; }
        public List<Assert> Asserts { get; set; }
    }
}
