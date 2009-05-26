using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class TextSpecGenerator : IGenerateSpecDocument
    {
        public string Output { get; private set; }

        #region IGenerateSpecDocument Members

        public void Generate(string scenario, SemanticModel.AAAMemento semanticModelState)
        {
            ThrowArgumentExceptionIfNull(semanticModelState, "semanticModelState");
        
            var content = new StringBuilder();

            content.Append(string.Format("Scenario: {0}\r\n", scenario));

            if (semanticModelState.Arranges.Count > 0)
                content.Append(string.Format("\tGiven {0}\r\n", semanticModelState.Arranges[0].Text));

            if (semanticModelState.Acts.Count > 0)
            {
                foreach (var actWithAsserts in semanticModelState.Acts)
                {
                    content.Append(string.Format("\tWhen {0}\r\n", actWithAsserts.Key.Text));
                    actWithAsserts.Value.ForEach(assert =>
                        content.Append(string.Format("\tThen {0}\r\n", assert.Text)));

                }
            }

            Output = content.ToString();
        }

        private void ThrowArgumentExceptionIfNull(Object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentException("Value can not be null", paramName);
        }

        #endregion
    }
}
