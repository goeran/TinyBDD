using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Services;

namespace TinyBDD
{
    public class StoryPartLogger : StoryPartDecorator
    {
        private ILog logger;

        public StoryPartLogger(StoryPart decoratedStoryPart, ILog logger) :
            base(decoratedStoryPart)
        {
            this.logger = logger;
        }

        public override void Execute()
        {
            base.Execute();

            if (decoratedStoryPart is Event)
            {
                var anEvent = decoratedStoryPart as Event;
                logger.Text(string.Format("When {0}", decoratedStoryPart.Title));

                int counter = 0;
                foreach (var outcome in anEvent.Outcome)
                {
                    if (counter == 0)
                        logger.Text(string.Format("Then {0}", outcome.Title));
                    else
                        logger.Text(string.Format("And {0}", outcome.Title));

                    counter++;
                }
            }
                
        }
    }
}
