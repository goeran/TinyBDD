using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
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
            semanticModelMemento.Arranges.Count.ShouldEqual(1);
        }

        [Test]
        public void Should_contain_acts()
        {
            semanticModelMemento.Acts.Count.ShouldEqual(2);
        }

        [Test]
        public void Acts_should_contain_asserts()
        {
            semanticModelMemento.Asserts.Count.ShouldEqual(2);
        }

        [Test]
        public void Should_be_able_to_execute()
        {
            semanticModel.Execute();
            output.ShouldEqual("ArrangeAct1Assert1ArrangeAct2Assert2");
        }

        [Test]
        public void Should_not_be_able_to_do_assertions_without_at_least_one_act()
        {
            var newSemanticModel = new AAA();
            newSemanticModel.Arrange("Arrange", () => { });

            this.ShouldThrowException<SemanticModelException>(() =>
                newSemanticModel.Assert("Assert", () => { }), ex =>
                    ex.Message.ShouldEqual("Can not assert without any acts specified"));
        }

        [Test]
        public void Should_rearrange_for_every_acts()
        {
            var number = 0;

            var newAAA = new AAA();

            newAAA.Arrange("Set start value", () =>
                number = 1);

            newAAA.Act("Multiply with two", () =>
                number *= 2);
            newAAA.Assert("Value should be two", () =>
                number.ShouldEqual(2));

            newAAA.Act("Multiply with four", () =>
                number *= 4);
            newAAA.Assert("Value should be four", () =>
                number.ShouldEqual(4));

            newAAA.Execute();

        }

    }
}
