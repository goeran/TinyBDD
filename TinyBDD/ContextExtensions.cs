using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl;

namespace TinyBDD
{
    public static class ContextExtensions
    {
        public static ContextSpecialCase And(this ContextSpecialCase context, string title)
        {
            return And(context, title, null);
        }

        public static ContextSpecialCase And(this ContextSpecialCase context, string title, Action action)
        {
            context.Scenario.AppendContext(title, action);
            return context;
        }
    }
}
