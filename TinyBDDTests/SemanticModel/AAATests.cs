using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD;
using TinyBDD.SemanticModel;

namespace TinyBDDTests.SemanticModel
{
    [TestFixture]
    public class AAATests
    {
        string output;
        AAA semanticModel;
        AAAMemento semanticModelMemento;

        [SetUp]
        public void Setup()
        {
            output = "";
            semanticModelMemento = new AAAMemento();
            semanticModel = new AAA(semanticModelMemento);

            semanticModel.Arrange("Arrange", () => output += "Arrange");

            semanticModel.Act("Act1", () => output += "Act1");

            semanticModel.Assert("Assert1", () => output += "Assert1");

            semanticModel.Act("Act2", () => output += "Act2");

            semanticModel.Assert("Assert2", () => output += "Assert2");

        }

        [Test]
        public void Should_contain_arrange()
        {
            semanticModelMemento.Arrange.ShouldNotBeNull();
        }

        [Test]
        public void Should_contain_acts()
        {
            semanticModelMemento.Acts.Count.ShouldEqual(2);
        }

        [Test]
        public void Acts_should_contain_asserts()
        {
            semanticModelMemento.Acts.Count.ShouldEqual(2);
        }

        [Test]
        public void Should_be_able_to_execute()
        {
            semanticModel.Execute();
            output.ShouldEqual("ArrangeAct1Assert1Act2Assert2");
        }
    }
}
