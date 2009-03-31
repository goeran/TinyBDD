using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Services;

namespace TinyBDD
{
    public class Event : StoryPart
    {
        private List<StoryPart> outcome;
        private ILog logger;

        public List<StoryPart> Outcome
        {
            get { return outcome; }
        }

        public Event()
        {
            outcome = new List<StoryPart>();
        }

        public void AddOutcome(string title, Action action)
        {
            outcome.Add(new Outcome()
            {
                Title = title,
                Action = action
            });
        }

        public override void Execute()
        {
            base.Execute();
            foreach (var outcome in Outcome)
                outcome.Execute();
        }

    }
}
