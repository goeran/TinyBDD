using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class AAA
    {
        string text;
        Arrange lastArrange;
        Act lastAct;
        AAAMemento memento;

        public AAA()
        {
            this.memento = new AAAMemento();
        }

        public AAA(AAAMemento memento)
            : this()
        {
            this.memento = memento;
        }

        public void Text(string text)
        {
            this.text = text;
            RememberText();
        }

        private void RememberText()
        {
            if (memento != null)
                memento.Text = text;
        }

        public void Arrange(string text, Action action)
        {
            lastArrange = new Arrange(text, action);

            RememberArrange();
        }

        private void RememberArrange()
        {
            if (memento != null)
                memento.Arranges.Add(this.lastArrange);
        }

        public void Act(string text, Action action)
        {
            lastAct = NewAct(text, action);

            RememberActs();
        }

        private Act NewAct(string text, Action action)
        {
            if (memento == null)
                return new Act(text, action);
            else
                return new Act(text, action, memento);
        }

        private void RememberActs()
        {
            if (memento != null)
                memento.Acts.Add(this.lastAct, new List<Assert>());
        }

        public void Assert(string text, Action action)
        {
            if (lastAct == null)
                throw new SemanticModelException("Can not assert without any acts specified");

            lastAct.Assert(text, action);
        }

        public void Execute()
        {
            foreach (var act in memento.Acts)
            {
                memento.Arranges.ForEach(arrange => arrange.Execute());
                act.Key.Execute();
            }
        }
    }
}
