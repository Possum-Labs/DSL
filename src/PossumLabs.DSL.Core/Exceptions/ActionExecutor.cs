using PossumLabs.DSL.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Exceptions
{
    public class ActionExecutor
    {
        public ActionExecutor(ILog logger)
        {
            Logger = logger; 
            IgnoredExceptions = new List<Exception>();
        }
        private ILog Logger { get; }
        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                if (ExpectException && Exception == null)
                {
                    Logger.Message($"expected exception will continue execution; caught '{e.ToString()}'");
                    Exception = e;
                }
                else if (ExpectException)
                    throw new AggregateException(new Exception($"One exception was expected, multiple were throw"), Exception, e);
                else
                    throw;
            }
        }

        /// <summary>
        /// For after scnearion hooks.
        /// </summary>
        public void ContinueOnError(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Exception = e;
                IgnoredExceptions.Add(e);
            }
        }

        public List<Exception> IgnoredExceptions { get; }

        public bool ExpectException { get; set; }

        public Exception Exception { get; set; }
    }
}
