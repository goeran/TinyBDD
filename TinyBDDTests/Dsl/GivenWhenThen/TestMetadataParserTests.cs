using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.Dsl.GivenWhenThen
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
    public class TestMetadataParserTests :Shared
    {
        [SetUp]
        public void Setup()
        {
            SetupTest();
        }

        Context there_are_changesets_in_sourceControl = () =>
        {
        };

        When latest_version_is_requested = () =>
        {
        };

        Then latest_changeset_should_be_downloaded = () =>
        {
        };

        [Test]
        public void Should_be_able_to_translate_Context_to_text()
        {
            var text = metadataParser.TranslateToText(there_are_changesets_in_sourceControl);

            text.ShouldBe("there are changesets in sourceControl");
        }

        [Test]
        public void Should_be_able_to_translate_When_to_text()
        {
            var text = metadataParser.TranslateToText(latest_version_is_requested);

            text.ShouldBe("latest version is requested");
        }

        [Test]
        public void Should_be_able_to_translate_Then_to_text()
        {
            var text = metadataParser.TranslateToText(latest_changeset_should_be_downloaded);

            text.ShouldBe("latest changeset should be downloaded");
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
        public void Shall_translate_ClassName_to_text()
        {
            var text = metadataParser.TranslateTestClassNameToText();

            text.ShouldBe("When parsing ClassName");
        }

        [Test]
        public void Shall_throw_ArgumentException_if_NullObj_is_passed()
        {
            this.ShouldThrowException<ArgumentException>(() =>
                new TestMetadataParser(null), exception =>
                    exception.Message.ShouldBe("Value can not be null"));
        }
    }
}
