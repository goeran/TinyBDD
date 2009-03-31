using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl;

namespace TinyBDD
{
    public static class ScenarioExtensions
    {
        public static ContextSpecialCase Given(this ScenarioSpecialCase scenario, string title, Action action)
        {
            scenario.Scenario.AppendContext(title, action);

            return new ContextSpecialCase()
            {
                Scenario = scenario.Scenario
            };
        }

        public static EventSpecialCase When(this ScenarioSpecialCase scenario, string title, Action action)
        {
            var anEvent = scenario.Scenario.AddEvent(title, action);

            return new EventSpecialCase()
            {
                Scenario = scenario.Scenario,
                Event = anEvent
            };
        }

        public static Scenario End(this ScenarioSpecialCase scenario)
        {
            return scenario.Scenario;
        }

        public static void Run(this ScenarioSpecialCase scenario)
        {
            scenario.Scenario.Run();
        }
    }
}
