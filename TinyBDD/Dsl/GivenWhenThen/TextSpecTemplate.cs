using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class TextSpecTemplate
    {
        public string GivenText { get; set; }
        public string AndText { get; set; }
        public string WhenText { get; set; }
        public string ThenText { get; set; }

        public TextSpecTemplate()
        {
            GivenText = "Given";
            AndText = "And";
            WhenText = "When";
            ThenText = "Then";
        }
    }
}
