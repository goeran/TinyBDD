using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;

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
                specGenerator.Generate(string.Empty, null), exception =>
                    exception.Message.ShouldBe("Value can not be null\r\nParameter name: semanticModelState"));
        }

        [Test]
        public void Should_write_scenario_to_the_output()
        {
            specGenerator.Generate(string.Empty, state);

            specGenerator.Output.ShouldBe("Scenario: \r\n");
        }

        [Test]
        public void Should_write_given_to_the_output()
        {
            semanticModel.Arrange("that there are changesets in SourceControl", () => { });

            specGenerator.Generate(string.Empty, state);

            specGenerator.Output.ShouldContain("Given that there are changesets in SourceControl\r\n");
        }
        
        [Test]
        public void Should_write_when_to_the_output()
        {
            semanticModel.Act("changesets are fetched", () => { });

            specGenerator.Generate(string.Empty, state);

            specGenerator.Output.ShouldContain("\tWhen changesets are fetched\r\n");
        }

        [Test]
        public void Should_write_then_to_the_output()
        {
            semanticModel.Act("changesets are fetched", () => { });
            semanticModel.Assert("latest changeset should be loaded into viewModel", () => { });

            specGenerator.Generate(string.Empty, state);

            specGenerator.Output.ShouldContain("\tThen latest changeset should be loaded into viewModel\r\n");
        }

        [Test]
        public void Should_write_the_whole_scenario_to_the_output()
        {
            semanticModel.Arrange("that there are changesets in sourceControl", () => { });
            semanticModel.Act("the user requests the latest version", () => { });
            semanticModel.Assert("user should get the latest changeset", () => { });
            semanticModel.Act("the user requests the first version", () => { });
            semanticModel.Assert("user should get the first changeset", () => { });

            specGenerator.Generate("Checkout", state);

            var expectedOutput = new StringBuilder();
            expectedOutput.Append("Scenario: Checkout\r\n");
            expectedOutput.Append("\tGiven that there are changesets in sourceControl\r\n");
            expectedOutput.Append("\tWhen the user requests the latest version\r\n");
            expectedOutput.Append("\tThen user should get the latest changeset\r\n");
            expectedOutput.Append("\tWhen the user requests the first version\r\n");
            expectedOutput.Append("\tThen user should get the first changeset\r\n");

            Console.WriteLine(expectedOutput.ToString());

            specGenerator.Output.ShouldBe(expectedOutput.ToString());
        }
    }
}
