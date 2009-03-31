using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD
{
    public class StoryPart
    {
        public string Title { get; set; }
        public Action Action { get; set; }

        public virtual void Execute()
        {
            if (Action != null)
                Action();
        }
    }
}
