using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using System.Reflection;
using TinyBDDTests.Extensions;

namespace TinyBDDTests.Dsl.GivenWhenThen.TestMetadataParserSpecs
{
    public class Shared
    {
        protected TestMetadataParser metadataParser;

        protected void SetupTest()
        {
            metadataParser = new TestMetadataParser(this);
        }
    }

    [TestFixture]
    public class When_parsing_Context_When_and_Then : Shared
    {
        [SetUp]
        public void Setup()
        {
            SetupTest();
        }

        private Context private_instance_Context = () => { };
        public Context public_instance_Context = () => { };
        internal Context internal_instance_Context = () => { };
        private static Context private_static_Context = () => { };
        public static Context public_static_Context = () => { };
        internal static Context internal_static_Context = () => { };

        [Test]
        public void Assure_Context_is_translated_to_text()
        {
            AssertCanBeTranslated("private instance Context", private_instance_Context);
            AssertCanBeTranslated("public instance Context", public_instance_Context);
            AssertCanBeTranslated("internal instance Context", internal_instance_Context);

            AssertCanBeTranslated("private static Context", private_static_Context);
            AssertCanBeTranslated("public static Context", public_static_Context);
            AssertCanBeTranslated("internal static Context", internal_static_Context);
        }

        private When private_instance_When = () => { };
        public When public_instance_When = () => { };
        internal When internal_instance_When = () => { };
        private static When private_static_When = () => { };
        public static When public_static_When = () => { };
        internal static When internal_static_When = () => { };

        [Test]
        public void Assure_When_is_translated_to_text()
        {
            AssertCanBeTranslated("private instance When", private_instance_When);
            AssertCanBeTranslated("public instance When", public_instance_When);
            AssertCanBeTranslated("internal instance When", internal_instance_When);
        
            AssertCanBeTranslated("private static When", private_static_When);
            AssertCanBeTranslated("public static When", public_static_When);
            AssertCanBeTranslated("internal static When", internal_static_When);
        }

        private Then private_instance_Then = () => { };
        public Then public_instance_Then = () => { };
        internal Then internal_instance_Then = () => { };
        private static Then private_static_Then = () => { };
        public static Then public_static_Then = () => { };
        internal static Then internal_static_Then = () => { };

        [Test]
        public void Assure_Then_is_translated_to_text()
        {
            AssertCanBeTranslated("private instance Then", private_instance_Then);
            AssertCanBeTranslated("public instance Then", public_instance_Then);
            AssertCanBeTranslated("internal instance Then", internal_instance_Then);

            AssertCanBeTranslated("private static Then", private_static_Then);
            AssertCanBeTranslated("public static Then", public_static_Then);
            AssertCanBeTranslated("internal static Then", internal_static_Then);
        }

        private void AssertCanBeTranslated(string expected_text, Delegate del)
        {
            var text = metadataParser.TranslateToText(del);

            text.ShouldBe(expected_text);
        }
    }

    [TestFixture]
    public class When_parsing_ClassName : Shared
    {
        [SetUp]
        public void Setup()
        {
            metadataParser = new TestMetadataParser(this);
        }

        [Test]
        public void Assure_ClassName_is_translated_to_text()
        {
            var text = metadataParser.TranslateTestClassNameToText();

            text.ShouldBe("When parsing ClassName");
        }

        [Test]
        public void Assure_ArgumentException_is_thrown_if_NullObj_is_passed_as_arg()
        {
            this.ShouldThrowException<ArgumentException>(() =>
                new TestMetadataParser(null), exception =>
                    exception.Message.ShouldBe("Value can not be null"));
        }
    }
}
