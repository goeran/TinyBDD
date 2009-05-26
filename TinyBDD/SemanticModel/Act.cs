using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class Act : Base
    {
        private Assert lastAssert;
        private List<Assert> asserts;
        private AAAMemento memento;

        public Act(string text, Action action)
        {
            Text = text;
            Action = action;
            asserts = new List<Assert>();
        }

        public Act(string text, Action action, AAAMemento memento)
            : this(text, action)
        {
            this.memento = memento;
        }

        public void Assert(string text, Action action)
        {
            lastAssert = new Assert(text, action);
            asserts.Add(lastAssert);
            RememberAsserts();
        }

        private void RememberAsserts()
        {
            if (memento != null)
                memento.Acts[this].Add(this.lastAssert);
        }

        public override void Execute()
        {
            base.Execute();
            asserts.ForEach(a => a.Execute());
        }
    }
}
