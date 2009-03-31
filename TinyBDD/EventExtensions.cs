using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl;

namespace TinyBDD
{
    public static class EventExtensions
    {

        public static OutcomeSpecialCase Then(this EventSpecialCase anEvent, string title, Action action)
        {
            anEvent.Event.AddOutcome(title, action);
            return new OutcomeSpecialCase()
            {
                Scenario = anEvent.Scenario,
                Event = anEvent.Event
            };
        }


    }
}
