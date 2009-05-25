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
    public class GenerateConsoleSpecTests
    {
        ConsoleSpecGenerator specGenerator;

        [SetUp]
        public void Setup()
        {
            specGenerator = new ConsoleSpecGenerator();
        }

        [Test]
        public void Generate_should_not_accept_null_pointer_as_argument()
        {
            this.ShouldThrowException<ArgumentException>(() =>
                specGenerator.Generate(null), exception =>
                    exception.Message.ShouldBe("Value can not be null\r\nParameter name: semanticModelState"));
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

            specGenerator.Output.ShouldNotBeNull();
        }
    }
}
