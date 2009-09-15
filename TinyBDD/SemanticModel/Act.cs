using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class Act : Base
    {
        private Assert lastAssert;
        private AAAMemento memento;

        public Act(string text, Action action)
        {
            Text = text;
            Action = action;
        }

        public Act(string text, Action action, AAAMemento memento)
            : this(text, action)
        {
            this.memento = memento;
        }

        public void Assert(string text, Action action)
        {
            lastAssert = new Assert(text, action);
            RememberAsserts();
        }

        private void RememberAsserts()
        {
            if (memento != null)
                memento.Acts[this].Add(lastAssert);
        }

        public override void Execute()
        {
            base.Execute();
            memento.Acts[this].ForEach(a => a.Execute());
        }
    }
}
