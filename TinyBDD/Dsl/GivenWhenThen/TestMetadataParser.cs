using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class TestMetadataParser
    {
        Object test;
        private const BindingFlags PRIVATE_INSTANCE_MEMBER = BindingFlags.Instance | BindingFlags.NonPublic;
        private const BindingFlags PRIVATE_STATIC_MEMBER = BindingFlags.Static | BindingFlags.NonPublic;
        private const BindingFlags PUBLIC_INSTANCE_MEMBER = BindingFlags.Instance | BindingFlags.Public;
        private const BindingFlags PUBLIC_STATIC_MEMBER = BindingFlags.Static | BindingFlags.Public;

        public TestMetadataParser(Object test)
        {
            ThrowArgumentExceptionIfArgIsNull(test);

            this.test = test;
        }

        private void ThrowArgumentExceptionIfArgIsNull(object test)
        {
            if (test == null)
                throw new ArgumentException("Value can not be null");
        }

        public string TranslateTestClassNameToText()
        {
            return FormatText(test.GetType().Name);
        }

        public string TranslateToText(Delegate variable)
        {
            var q = QueryTestForPrivateField(variable.GetType(), variable);
            return FormatTitleIfFieldFound(q);
        }

        private FieldInfo QueryTestForPrivateField(Type fieldType, Object fieldValue)
        {
            var retValue = TryGetMember(PRIVATE_INSTANCE_MEMBER, fieldType, fieldValue);

            if (retValue == null)
                retValue = TryGetMember(PUBLIC_INSTANCE_MEMBER, fieldType, fieldValue);

            if (retValue == null)
                retValue = TryGetMember(PRIVATE_STATIC_MEMBER, fieldType, fieldValue);

            if (retValue == null)
                retValue = TryGetMember(PUBLIC_STATIC_MEMBER, fieldType, fieldValue);

            return retValue;
        }

        private FieldInfo TryGetMember(BindingFlags bindingFlags, Type fieldType, 
            object fieldValue)
        {
            return test.GetType().GetFields(bindingFlags).
                Where(field => field.FieldType == fieldType && field.GetValue(test) == fieldValue && !field.Name.StartsWith("CS$")).
                FirstOrDefault();
        }

        private string FormatTitleIfFieldFound(FieldInfo field)
        {
            if (field != null)
                return FormatText(field.Name);
            else
                return string.Empty;
        }

        private string FormatText(string title)
        {
            title = title.Replace("_", " ");
            return title;
        }
    }
}
