using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class Act : Base
    {
        private List<Assert> asserts;
        private AAAMemento memento;

        public Act(string text, Action action)
        {
            Title = text;
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
            asserts.Add(new Assert(text, action));
            RememberAsserts();
        }

        private void RememberAsserts()
        {
            if (memento != null)
                memento.Asserts = this.asserts;
        }

        public override void Execute()
        {
            base.Execute();
            asserts.ForEach(a => a.Execute());
        }
    }
}
