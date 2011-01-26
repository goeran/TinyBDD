using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDDTests.Extensions;
using Assert = NUnit.Framework.Assert;

namespace TinyBDDTests.Dsl.GivenWhenThen
{
    [TestFixture]
    public class TextSpecGeneratorTests
    {
        TextSpecGenerator specGenerator;
        AAAMemento state;
        AAA semanticModel;

        [SetUp]
        public void Setup()
        {
            state = new AAAMemento();
            semanticModel = new AAA(state);
            specGenerator = new TextSpecGenerator();
        }

        [Test]
        public void Generate_should_not_accept_null_pointer_as_argument()
        {
            this.ShouldThrowException<ArgumentException>(() =>
                specGenerator.Generate(null), exception =>
                    exception.Message.ShouldBe("Value can not be null\r\nParameter name: semanticModelState"));
        }

        [Test]
        public void Should_write_scenario_to_the_output()
        {
            specGenerator.Generate(state);

            specGenerator.Output.ShouldBe("Scenario: \r\n");
        }

        [Test]
        public void Shall_write_scenario_Name_to_the_output()
        {
            semanticModel.Text("Scenario name goes here");

            specGenerator.Generate(state);

            specGenerator.Output.StringShouldContain("Scenario: Scenario name goes here");
        }
        

        [Test]
        public void Should_write_given_to_the_output()
        {
            semanticModel.Arrange("that there are changesets in SourceControl", () => { });

            specGenerator.Generate(state);

            specGenerator.Output.StringShouldContain("Given that there are changesets in SourceControl\r\n");
        }
        
        [Test]
        public void Should_write_when_to_the_output()
        {
            semanticModel.Act("changesets are fetched", () => { });

            specGenerator.Generate(state);

            specGenerator.Output.StringShouldContain("\tWhen changesets are fetched\r\n");
        }

        [Test]
        public void Should_write_then_to_the_output()
        {
            semanticModel.Act("changesets are fetched", () => { });
            semanticModel.Assert("latest changeset should be loaded into viewModel", () => { });

            specGenerator.Generate(state);

            specGenerator.Output.StringShouldContain("\tThen latest changeset should be loaded into viewModel\r\n");
        }

        [Test]
        public void Should_write_nested_given_to_the_output()
        {
            semanticModel.Arrange("there are changesets in sourceControl", () => { });
            semanticModel.Arrange("user have permission to read", () => { });
            semanticModel.Arrange("sourceControl is available", () => { });

            specGenerator.Generate(state);

            var expectedOutput = new StringBuilder();
            expectedOutput.Append("\tGiven there are changesets in sourceControl\r\n");
            expectedOutput.Append("\tAnd user have permission to read\r\n");
            expectedOutput.Append("\tAnd sourceControl is available\r\n");

            specGenerator.Output.StringShouldContain(expectedOutput.ToString());
        }

        [Test]
        public void Should_write_nested_asserts_to_the_output()
        {
            semanticModel.Arrange("there are changesets in sourceControl", () => { });
            semanticModel.Act("the latest version is requested", () => { });
            semanticModel.Assert("ensure the latest version is downloaded", () => { });
            semanticModel.Assert("ensure no files are edited", () => { });

            specGenerator.Generate(state);

            var expectedOutput = new StringBuilder();
            expectedOutput.Append("\tThen ensure the latest version is downloaded\r\n");
            expectedOutput.Append("\tAnd ensure no files are edited");

            specGenerator.Output.StringShouldContain(expectedOutput.ToString());
        }

        [Test]
        public void Should_write_the_whole_scenario_to_the_output()
        {
            semanticModel.Text("Checkout");
            semanticModel.Arrange("that there are changesets in sourceControl", () => { });
            semanticModel.Act("the user requests the latest version", () => { });
            semanticModel.Assert("user should get the latest changeset", () => { });
            semanticModel.Act("the user requests the first version", () => { });
            semanticModel.Assert("user should get the first changeset", () => { });

            specGenerator.Generate(state);

            var expectedOutput = new StringBuilder();
            expectedOutput.Append("Scenario: Checkout\r\n");
            expectedOutput.Append("\tGiven that there are changesets in sourceControl\r\n");
            expectedOutput.Append("\tWhen the user requests the latest version\r\n");
            expectedOutput.Append("\tThen user should get the latest changeset\r\n");
            expectedOutput.Append("\tWhen the user requests the first version\r\n");
            expectedOutput.Append("\tThen user should get the first changeset\r\n");

            specGenerator.Output.ShouldBe(expectedOutput.ToString());
        }

        [Test]
        public void Shall_be_possible_to_specify_Template()
        {
            semanticModel.Arrange("that there are changesets in sourceControl", () => { });
            semanticModel.Arrange("user have access", () => { });
            semanticModel.Act("user checkout repository", () => { });
            semanticModel.Assert("latest version is returned", () => { });

            specGenerator = new TextSpecGenerator(new TextSpecTemplate()
            {
                GivenText = "Gitt",
                AndText = "Og",
                ThenText = "Så",
                WhenText = "Når"
            });

            specGenerator.Generate(state);

            specGenerator.Output.StringShouldContain("Gitt that there are changesets in sourceControl");
            specGenerator.Output.StringShouldContain("Og user have access");
            specGenerator.Output.StringShouldContain("Når user checkout repository");
            specGenerator.Output.StringShouldContain("Så latest version is returned");
        }
    }
}
