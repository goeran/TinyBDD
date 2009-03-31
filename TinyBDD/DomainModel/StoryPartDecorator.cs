using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD
{
    public abstract class StoryPartDecorator : StoryPart
    {
        protected StoryPart decoratedStoryPart;

        public StoryPartDecorator(StoryPart decoratedStoryPart)
        {
            this.decoratedStoryPart = decoratedStoryPart;
            this.Title = decoratedStoryPart.Title;
        }

        public override void Execute()
        {
            decoratedStoryPart.Execute();
        }
    }
}
