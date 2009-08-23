using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.RunnerTests
{
    public delegate void Scenario();
    public delegate void Context();
    public delegate void When();
    public delegate void Then();

    public class Behaviour
    {
        public static void Given(Context context)
        {
            context();
        }
        public static void When(When when)
        {
            when();
        }

        public static void Then(string text)
        {
            
        }

        public static void Then(Then then)
        {
            then();
        }
    }

    public class When_getting_latest_version : Behaviour
    {
        static Context there_are_changesets_in_SourceControl = () => {};
        static When changelog_is_reguested = () => { };

        Scenario get_latest_version = () =>
        {
            Given(there_are_changesets_in_SourceControl);
            
            When(changelog_is_reguested);

            Then("assure resultset contains data");
        };

    }
}
