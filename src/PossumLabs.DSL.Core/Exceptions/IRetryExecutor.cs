using System;

namespace PossumLabs.DSL.Core.Exceptions
{
    public interface IRetryExecutor
    {
        void RetryFor(Action a, TimeSpan retryDuration);
        T RetryFor<T>(Func<T> func, TimeSpan retryDuration);
    }
}