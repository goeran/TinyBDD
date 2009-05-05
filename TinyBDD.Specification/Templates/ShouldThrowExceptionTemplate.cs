using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBDD.Specification.Templates
{
    public class ShouldThrowExceptionTemplate<T> where T : Exception
    {
        Action invoke;
        Action<string> assertFail;
        Action<T> invokeException;

        public ShouldThrowExceptionTemplate(Action invoke, Action<string> assertFail, Action<T> invokeException)
        {
            this.invoke = invoke;
            this.assertFail = assertFail;
            this.invokeException = invokeException;
        }

        #region IAssertTemplate Members

        public void Execute()
        {
            Exception ex = null;

            try
            {
                invoke();
            }
            catch (Exception e)
            {
                ex = e;
            }

            if (ex == null)
                assertFail.Invoke("Did not throw exception");
            else
            {
                if (ex.GetType() != typeof(T))
                    assertFail.Invoke(
                        string.Format("Did not throw expected exception ({0}), but {1}", typeof(T).Name, ex.GetType().Name));
                else
                    invokeException.Invoke((T)ex);
            }

        }

        #endregion
    }
}
