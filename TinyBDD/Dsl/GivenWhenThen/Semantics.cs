using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public delegate void Context();

    public delegate void When();

    public class Semantics
    {
        Object test;
        SemanticModel.AAA semanticModel;

        public Semantics(Object test, SemanticModel.AAA semanticModel)
        {
            this.test = test;
            this.semanticModel = semanticModel;
        }

        public GivenSemantics Given(Context context)
        {
            return Given(TranslateToTitle(context), () => { context(); });
        }

        private string TranslateToTitle(Delegate context)
        {
            var q = QueryTestForPrivateFields(typeof(Context), context);
            return FormatTitleIfFieldFound(q);
        }

        private IEnumerable<FieldInfo> QueryTestForPrivateFields(Type fieldType, Object fieldValue)
        {
            var retValue = test.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(field => field.FieldType == fieldType && field.GetValue(test) == fieldValue);
            return retValue;
        }

        private string FormatTitleIfFieldFound(IEnumerable<FieldInfo> q)
        {
            if (q.Count() > 0)
                return FormatTitle(q.Single().Name);
            else
                return string.Empty;
        }

        private string FormatTitle(string title)
        {
            title = title.Replace("_", " ");
            return title;
        }

        public GivenSemantics Given(string text)
        {
            return Given(text, () => { });
        }

        public GivenSemantics Given(string text, Action action)
        {
            var givenSemantics = new GivenSemantics(semanticModel);

            semanticModel.Arrange(text, action);

            return givenSemantics;
        }

        public void When(When when)
        {
            When(TranslateToTitle(when), () => { when(); });
        }

        private string TranslateToTitle(When when)
        {
            var q = QueryTestForPrivateFields(typeof(When), when);
            return FormatTitleIfFieldFound(q);
        }

        public void When(string text)
        {
            semanticModel.Act(text, () => { });
        }

        public void When(string text, Action action)
        {
            semanticModel.Act(text, action);
        }

        public ThenSemantics Then(string text, Action action)
        {
            semanticModel.Assert(text, action);

            return new ThenSemantics(semanticModel);
        }

   }
}
