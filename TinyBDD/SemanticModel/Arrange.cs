using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class Arrange : Base
    {
        public Arrange(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }
}
