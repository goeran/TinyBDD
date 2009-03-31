using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Services;
using TinyBDD.Factories;
using TinyBDD.Dsl;

namespace TinyBDD
{
    public class Scenario
    {
        private List<Context> context;
        private List<StoryPart> events;
        private ILog logger;

        public string Title { get; set; }

        public List<Context> Context
        {
          get { return context; }
        }
        public IEnumerable<StoryPart> Events
        {
            get 
            {
                return events.AsEnumerable();
            }
        }

        public Scenario()
        {
            context = new List<Context>();
            events = new List<StoryPart>();
        }

        public Scenario(ILog logger) : 
            this()
        {
            this.logger = logger;
        }

        public void AppendContext(string title, Action action)
        {
            context.Add(new Context()
                {
                    Title = title,
                    Action = action
                });
        }

        public Event AddEvent(string title, Action action)
        {
            var anEvent = new Event()
            {
                Title = title,
                Action = action
            };

            if (logger != null)
                events.Add(new StoryPartLogger(anEvent, logger));
            else
                events.Add(anEvent);

            return anEvent;
        }

        public void Run()
        {
            Log(string.Format("Scenario: {0}", Title));
            ExecuteEvents();
            Log("");
        }

        private void ExecuteContext()
        {
            int counter = 0;
            foreach (var item in context)
            {
                if (counter == 0)
                    Log(string.Format("Given {0}", item.Title));
                else
                    Log(string.Format("And {0}", item.Title));
                if (item.Action != null)
                    item.Action.Invoke();
                counter++;
            }
        }

        private void Log(string message)
        {
            if (logger != null)
                logger.Text(message);
        }

        private void ExecuteEvents()
        {
            foreach (var anEvent in Events)
            {
                ExecuteContext();
                anEvent.Execute();
            }
        }

        public static Scenario New(string title)
        {
            return Factory.NewScenario(title);
        }

        public static ScenarioSpecialCase New(string title, Action<ScenarioSpecialCase> body)
        {
            var scenario = Factory.NewScenario(title);
            var scenarioSpecial = new ScenarioSpecialCase()
                {
                    Scenario = scenario
                };
            body.Invoke(scenarioSpecial);

            return scenarioSpecial;
        }

        private static ScenarioFactory _scenarioFactory = new ScenarioFactory();
        internal static ScenarioFactory Factory 
        {
            get { return _scenarioFactory; }
            set { _scenarioFactory = value; }
        }
    }
}
