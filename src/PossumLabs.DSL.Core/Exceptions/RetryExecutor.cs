using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace PossumLabs.DSL.Core.Exceptions
{
    public class RetryExecutor : IRetryExecutor
    {

        public void RetryFor(Action a, TimeSpan retryDuration)
            => RetryFor<int>(() => { a(); return 42; }, retryDuration);

        public T RetryFor<T>(Func<T> func, TimeSpan retryDuration)
        {
            var sw = Stopwatch.StartNew();
            var exceptions = new List<Exception>();
            var retries = 0;
            while (sw.Elapsed < retryDuration)
            {
                try
                {
                    retries++;
                    return func();
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }
            //need to do it one more time as we might elapse midway trough a try
            try
            {
                retries++;
                return func();
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
            var uniqueErrors = exceptions.GroupBy(e => e.Message).Select(e => e.First());
            throw new AggregateException($"Retries failed, tried {retries} times, got {uniqueErrors.Count()} error", uniqueErrors);
        }
    }
}
