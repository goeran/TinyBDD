using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.SemanticModel;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyBDD.Dsl.GivenWhenThenNO
{
    public class Scenario
    {
        public AAAMemento Tilstand { get; private set; }
        private AAA semantiskModel;

        private Scenario()
        {
            Tilstand = new AAAMemento();
            semantiskModel = new AAA(Tilstand);
        }

        public static Scenario Nytt(string scenarioNavn, Object test, Action<Semantikk> scenario)
        {
            var returverdi = new Scenario();
            scenario(new Semantikk(returverdi.semantiskModel, test));
            return returverdi;
        }

        public static void Start(string scenarioNavn, Object test, Action<Semantikk> scenario)
        {
            var nyttScenario = new Scenario();
            scenario(new Semantikk(nyttScenario.semantiskModel, test));

            var tekstSpesifikasjonGenerator = new TextSpecGenerator(new TextSpecTemplate()
            {
                GivenText = "Gitt",
                AndText = "Og",
                WhenText = "Når",
                ThenText = "Så"
            });

            tekstSpesifikasjonGenerator.Generate(nyttScenario.Tilstand);
            Console.WriteLine(tekstSpesifikasjonGenerator.Output);

            nyttScenario.semantiskModel.Execute();
        }


        public static implicit operator AAAMemento(Scenario scenario)
        {
            return scenario.Tilstand;
        }
    }
}
