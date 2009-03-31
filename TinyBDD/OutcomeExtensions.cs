using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl;

namespace TinyBDD
{
    public static class OutcomeExtensions
    {
        public static OutcomeSpecialCase And(this OutcomeSpecialCase outcome, string title, Action action)
        {
            outcome.Event.AddOutcome(title, action);
            return outcome;
        }
    }
}
