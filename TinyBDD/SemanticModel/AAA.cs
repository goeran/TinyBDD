﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class AAA
    {
        Arrange lastArrange;
        List<Arrange> arranges;
        List<Act> acts;
        Act lastAct;
        AAAMemento memento;

        public AAA()
        {
            arranges = new List<Arrange>();
            acts = new List<Act>();
        }

        public AAA(AAAMemento memento)
            : this()
        {
            this.memento = memento;
        }

        public void Arrange(string text, Action action)
        {
            lastArrange = new Arrange(text, action);
            arranges.Add(lastArrange);

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
            acts.Add(lastAct);

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
            if (acts != null)
                memento.Acts.Add(this.lastAct);
        }

        public void Assert(string text, Action action)
        {
            lastAct.Assert(text, action);
        }

        public void Execute()
        {
            arranges.ForEach(a => a.Execute());
            acts.ForEach(a => a.Execute());
        }
    }
}
