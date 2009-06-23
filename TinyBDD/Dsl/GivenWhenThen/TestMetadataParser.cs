﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyBDD.Dsl.GivenWhenThen
{
    public class TestMetadataParser
    {
        Object test;

        public TestMetadataParser(Object test)
        {
            this.test = test;
        }

        public string TranslateToText(Delegate variable)
        {
            var q = QueryTestForPrivateFields(variable.GetType(), variable);
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
    }
}
