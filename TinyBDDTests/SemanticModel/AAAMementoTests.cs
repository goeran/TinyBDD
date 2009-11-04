using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.SemanticModel;
using Assert=TinyBDD.SemanticModel.Assert;

using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.SemanticModel
{
    [TestFixture]
    public class AAAMementoTests
    {
        [Test]
        public void Assure_its_cloneable()
        {
            var state = new AAAMemento();
            state.Text = "When login";
            state.Arranges.Add(new Arrange("ViewModel is created", () => { }));
            state.Acts.Add(new Act("login", () => {}), new List<Assert>());
            state.Acts.First().Value.Add(new Assert("assure credentials are validated against the AuthService", () => { }));

            var stateCopy = state.Copy() as AAAMemento;

            stateCopy.Text.ShouldBe(state.Text);
            stateCopy.Arranges.ShouldNotBeNull();
            stateCopy.Arranges.Count.ShouldBe(state.Arranges.Count);
            foreach (var arrange in state.Arranges)
                stateCopy.Arranges.ShouldContain(arrange);

            stateCopy.Acts.ShouldNotBeNull();
            stateCopy.Acts.Count.ShouldBe(state.Acts.Count);
            foreach (var act in state.Acts)
            {
                stateCopy.Acts.Keys.ShouldContain(act.Key);
                stateCopy.Acts.Values.ShouldContain(act.Value);
            }
        }
    }
}
