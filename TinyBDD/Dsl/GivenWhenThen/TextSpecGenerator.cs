using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class TextSpecGenerator : IGenerateSpecDocument
    {
        private TextSpecTemplate textSpecTemplate;

        public string GivenTemplate { get; set; }
        public string Output { get; private set; }

        public TextSpecGenerator() :
            this(new TextSpecTemplate())
        {
        }

        public TextSpecGenerator(TextSpecTemplate textSpecTemplate)
        {
            this.textSpecTemplate = textSpecTemplate;
        }

        #region IGenerateSpecDocument Members

        public void Generate(string scenario, SemanticModel.AAAMemento semanticModelState)
        {
            ThrowArgumentExceptionIfNull(semanticModelState, "semanticModelState");
        
            var content = new StringBuilder();

            WriteScenario(scenario, content);

            WriteGivens(semanticModelState, content);

            WriteWhens(semanticModelState, content);

            Output = content.ToString();
        }

        private void ThrowArgumentExceptionIfNull(Object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentException("Value can not be null", paramName);
        }

        private void WriteScenario(string scenario, StringBuilder content)
        {
            content.Append(string.Format("Scenario: {0}\r\n", scenario));
        }

        private void WriteGivens(SemanticModel.AAAMemento semanticModelState, StringBuilder content)
        {
            if (semanticModelState.Arranges.Count > 0)
                foreach (var arrange in semanticModelState.Arranges)
                    if (arrange == semanticModelState.Arranges.First())
                        content.Append(string.Format("\t{0} {1}\r\n", textSpecTemplate.GivenText, arrange.Text));
                    else
                        content.Append(string.Format("\t{0} {1}\r\n", textSpecTemplate.AndText, arrange.Text));
        }

        private void WriteWhens(SemanticModel.AAAMemento semanticModelState, StringBuilder content)
        {
            if (semanticModelState.Acts.Count > 0)
                foreach (var actWithAsserts in semanticModelState.Acts)
                {
                    content.Append(string.Format("\t{0} {1}\r\n", textSpecTemplate.WhenText, actWithAsserts.Key.Text));
                    WriteThens(actWithAsserts.Value, content);
                }
        }

        private void WriteThens(List<SemanticModel.Assert> asserts, StringBuilder content)
        {
            var counter = 0;
            asserts.ForEach(assert =>
            {
                if (counter == 0)
                    content.Append(string.Format("\t{0} {1}\r\n", textSpecTemplate.ThenText, assert.Text));
                else
                    content.Append(string.Format("\t{0} {1}\r\n", textSpecTemplate.AndText, assert.Text));
                counter++;
            });
        }

        #endregion
    }
}
