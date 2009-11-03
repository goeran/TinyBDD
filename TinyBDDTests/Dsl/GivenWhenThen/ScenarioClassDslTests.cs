using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using NUnit.Framework;

namespace TinyBDDTests.Dsl.GivenWhenThen.ScenarioClassDslTests
{
    public class Shared : ScenarioClass
    {
        protected Context there_are_changesets_in_sourceControl = () => { };
        protected When Controller_notified_to_refresh = () => { };
        protected Then assure_all_changesets_are_received = () => { };

        [TearDown]
        public void Haldis()
        {
            StartScenario();
        }
    }

    [TestFixture]
    public class When_describing_Scenario : Shared
    {
        [SetUp]
        public void Setup()
        {
            Given(there_are_changesets_in_sourceControl).
                And("controller has been created");

            When(Controller_notified_to_refresh);

            Then(assure_all_changesets_are_received).
                And("they are ordered by Revision");

        }
    
        [Test]
        public void Assure_arranges_been_added_to_the_SemanticModel()
        {
            //Run();
        }
    }

    [TestFixture]
    public class When_describing_empty_Scenario : Shared
    {
        [Test]
        public void Test()
        {
            Scenario("");
            Given("there are changesets in sourceControl");
            //Run();
        }
    }
}
