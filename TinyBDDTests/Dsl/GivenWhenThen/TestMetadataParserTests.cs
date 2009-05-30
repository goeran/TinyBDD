using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.Dsl.GivenWhenThen
{
    [TestFixture]
    public class TestMetadataParserTests
    {
        TestMetadataParser metadataParser;

        [SetUp]
        public void Setup()
        {
            metadataParser = new TestMetadataParser(this);
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
}
