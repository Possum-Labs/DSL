using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Core.Exceptions
{
    public interface IActionExecutor
    {
        Exception Exception { get; set; }
        bool ExpectException { get; set; }
        List<Exception> IgnoredExceptions { get; }

        void ContinueOnError(Action action);
        void Execute(Action action);
        T ReturnNullWhenErrorOccured<T>(Func<T> func);
    }
}