﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public delegate void Context();

    public delegate void When();

    public delegate void Then();

    public class Semantics
    {
        Object test;
        SemanticModel.AAA semanticModel;
        TestMetadataParser metadataParser;

        public Semantics(Object test, SemanticModel.AAA semanticModel)
        {
            this.test = test;
            this.semanticModel = semanticModel;
            metadataParser = new TestMetadataParser(test);
        }

        public GivenSemantics Given(Context context)
        {
            return Given(metadataParser.TranslateToText(context), () => { context(); });
        }

        public GivenSemantics Given(string text)
        {
            return Given(text, () => { });
        }

        public GivenSemantics Given(string text, Action action)
        {
            var givenSemantics = new GivenSemantics(this.test, semanticModel);

            semanticModel.Arrange(text, action);

            return givenSemantics;
        }

        public void When(When when)
        {
            When(metadataParser.TranslateToText(when), () => { when(); });
        }

        public void When(string text)
        {
            semanticModel.Act(text, () => { });
        }

        public void When(string text, Action action)
        {
            semanticModel.Act(text, action);
        }

        public ThenSemantics Then(Then then)
        {
            return Then(metadataParser.TranslateToText(then),
                () => { then(); });
        }

        public ThenSemantics Then(string text)
        {
            return Then(text, () => { });
        }

        public ThenSemantics Then(string text, Action action)
        {
            semanticModel.Assert(text, action);

            return new ThenSemantics(test, semanticModel);
        }

   }
}
