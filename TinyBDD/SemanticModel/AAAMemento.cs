using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.SemanticModel
{
    public class AAAMemento 
    {
        public string Text { get; set; }
        public List<Arrange> Arranges { get; set; }
        public Dictionary<Act, List<Assert>> Acts { get; set; }

        public AAAMemento()
        {
            Arranges = new List<Arrange>();
            Acts = new Dictionary<Act, List<Assert>>();
        }

        #region ICloneable Members

        public AAAMemento Copy()
        {
            var newMemento = new AAAMemento();
            newMemento.Text = Text;
            newMemento.Arranges.AddRange(Arranges);

            foreach (var act in Acts)
                newMemento.Acts.Add(act.Key, act.Value);

            return newMemento;
       }

        #endregion
    }
}
