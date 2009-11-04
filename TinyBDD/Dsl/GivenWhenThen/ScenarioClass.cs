using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class ScenarioClass
    {
        private AAA semanticModel;
        private AAAMemento semanticModelState;
        private Semantics scenario;
        private TextSpecGenerator specGenerator;
        private TestMetadataParser metadataParser;

        public ScenarioClass() : 
            this(new AAAMemento())
        {
        }

        public ScenarioClass(AAAMemento semanticModelState)
        {
            this.semanticModel = new AAA(semanticModelState);
            this.semanticModelState = semanticModelState;
            this.scenario = new Semantics(this, semanticModel);
            this.specGenerator = new TextSpecGenerator();
            this.metadataParser = new TestMetadataParser(this);

            semanticModelState.Text = metadataParser.TranslateTestClassNameToText();
        }

        public void Scenario(string text)
        {
            semanticModelState.Text = text;
            ClearSemanticModelState();
        }

        private void ClearSemanticModelState()
        {
            semanticModelState.Arranges.Clear();
            semanticModelState.Acts.Clear();
        }

        public GivenSemantics Given(string text)
        {
            return scenario.Given(text);
        }

        public GivenSemantics Given(string text, Action action)
        {
            return scenario.Given(text, action);
        }

        public GivenSemantics Given(Context context)
        {
            return scenario.Given(context);
        }

        public GivenSemantics Given(GivenSemantics semantics)
        {
            return semantics;
        }

        public void When(string text)
        {
            scenario.When(text);
        }

        public void When(string text, Action action)
        {
            scenario.When(text, action);
        }

        public void When(When when)
        {
            scenario.When(when);
        }

        public ThenSemantics Then(string text)
        {
            return scenario.Then(text);
        }

        public ThenSemantics Then(string text, Action action)
        {
            return scenario.Then(text, action);
        }

        public ThenSemantics Then(Then then)
        {
            return scenario.Then(then);
        }

        public ThenSemantics Then(ThenSemantics semantics)
        {
            return semantics;
        }

        public AndSemantics And(Context context)
        {
            semanticModel.Arrange(metadataParser.TranslateToText(context), () => { context(); });
            return new AndSemantics(this, semanticModel);   
        }

        public AndSemantics And(Then then)
        {
            semanticModel.Assert(metadataParser.TranslateToText(then), () => { then(); });
            return new AndSemantics(this, semanticModel);   
        }

        public AndSemantics And(string text)
        {
            return And(text, () => { });
        }

        public AndSemantics And(string text, Action a)
        {
            if (semanticModelState.Acts.Count == 0)
                semanticModel.Arrange(text, a);
            else
                semanticModel.Assert(text, a);

            return new AndSemantics(this, semanticModel);   
        }

        public AndSemantics And(AndSemantics semantics)
        {
            return semantics;
        }

        public void StartScenario()
        {
            SemanticModelState(semanticModelState.Copy());

            specGenerator.Generate(semanticModelState);
            Console.WriteLine(specGenerator.Output);
            semanticModel.Execute();

            ClearSemanticModelState();
        }

        protected virtual void SemanticModelState(AAAMemento semanticModelState)
        {
        }
    }
}
