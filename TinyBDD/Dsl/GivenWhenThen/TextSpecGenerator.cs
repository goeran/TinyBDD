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

        public void Generate(SemanticModel.AAAMemento semanticModelState)
        {
            ThrowArgumentExceptionIfNull(semanticModelState, "semanticModelState");
        
            var content = new StringBuilder();

            if (semanticModelState.Arranges.Count > 0)
                content.Append(string.Format("Given {0}\r\n", semanticModelState.Arranges[0].Title));

            if (semanticModelState.Acts.Count > 0)
                semanticModelState.Acts.ForEach(a =>
                    content.Append(string.Format("\twhen {0}\r\n", a.Title)));

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
