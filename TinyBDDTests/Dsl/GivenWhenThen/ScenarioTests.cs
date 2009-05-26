﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.Dsl.GivenWhenThen
{
    [TestFixture]
    public class ScenarioTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Should_create_scenario_and_execute_it()
        {
            var executed = false;
            var semanticModel = Scenario.StartNew(this, scenario =>
            {
                scenario.Given("there are changesets in sourceControl");

                scenario.When("changesets are loaded", () => 
                    executed = true);
            });

            executed.ShouldBeTrue();
        }

        [Test]
        public void Should_create_scenario_and_execute_it2()
        {
            var executed = false;
            var semanticModel = Scenario.StartNew(this, "load changesets from sourcecontrol", scenario =>
            {
                scenario.Given("there are changesets in sourceControl");

                scenario.When("changesets are loaded", () =>
                    executed = true);
            });

            executed.ShouldBeTrue();
        
        }
    }
}
