using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Services;

namespace TinyBDD.Services.Impl
{
    public class ConsoleLogger : ILog
    {
        #region ILog Members

        public void Text(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }
}
