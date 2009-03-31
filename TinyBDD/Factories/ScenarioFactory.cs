using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Services.Impl;

namespace TinyBDD.Factories
{
    public class ScenarioFactory
    {
        public Scenario NewScenario(string title)
        {
            return new Scenario(
                new ConsoleLogger())
                {
                    Title = title
                };
        }
    }
}
