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
    public class TextSpecGeneratorTests
    {
        TextSpecGenerator specGenerator;

        [SetUp]
        public void Setup()
        {
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
            var semanticModel = Scenario.New(this, scenario =>
            {
            });

            specGenerator.Generate(semanticModel.State);

            specGenerator.Output.ShouldBe(string.Empty);
        }

        [Test]
        public void Should_write_given_to_the_output()
        {
            var semanticModel = Scenario.New(this, scenario =>
            {
                scenario.Given("that there are changesets in SourceControl");

                scenario.When("Hello!");
            });


            specGenerator.Generate(semanticModel.State);

            specGenerator.Output.ShouldContain("Given that there are changesets in SourceControl\r\n");
        }
        
        [Test]
        public void Should_write_when_to_the_output()
        {
            var semanticModel = Scenario.New(this, scenario =>
            {
                scenario.When("changesets are fetched");
            });

            specGenerator.Generate(semanticModel.State);

            specGenerator.Output.ShouldContain("\twhen changesets are fetched\r\n");
        }

        [Test]
        [Ignore]
        public void Should_write_then_to_the_output()
        {
            var semanticModel = Scenario.New(this, scenario =>
            {
                scenario.When("changesets are fetched");

                scenario.Then("latest changeset should be loaded into viewModel", () => { });
            });

            specGenerator.Generate(semanticModel.State);

            specGenerator.Output.ShouldContain("\tthen latest changeset should be loaded into viewModel\r\n");
        }
    }
}
