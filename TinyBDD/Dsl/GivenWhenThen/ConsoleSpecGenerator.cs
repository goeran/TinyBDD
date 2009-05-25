using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ConsoleSpecGenerator : IGenerateSpecDocument
    {
        public string Output { get; private set; }

        #region IGenerateSpecDocument Members

        public void Generate(SemanticModel.AAAMemento semanticModelState)
        {
            ThrowArgumentExceptionIfNull(semanticModelState, "semanticModelState");
        
            var content = new StringBuilder();
            content.Append(semanticModelState.Arranges[0].Title);

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
