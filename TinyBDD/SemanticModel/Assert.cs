using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class Assert : Base
    {
        public Assert(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }
}
