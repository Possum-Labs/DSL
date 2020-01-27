using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PossumLabs.DSL.Core.Threading
{
    public static class ParallelFirstOrExceptionExtension
    {
        public class Wrapper<I, T>
        {
            public I Input { get; set; }
            public T Item { get; set; }
            public Exception Exception { get; set; }
            public long DurationMs { get; set; }
        }

        public static Wrapper<I, T> ParallelFirstOrException<I, T>(this IEnumerable<I> l, Func<I, T> func, Predicate<T> test)
        {
            var swt = Stopwatch.StartNew();
            var wrappers = l.Select(i => new Wrapper<I, T>() { Input = i }).ToArray();
            var state = Parallel.ForEach(wrappers,
                //new ParallelOptions { MaxDegreeOfParallelism = 4 }, 
                (wrapper, loopState) =>
                {
                    var sw = Stopwatch.StartNew();
                    try
                    {
                        var r = func(wrapper.Input);
                        if (test(r))
                        {
                            wrapper.Item = r;
                            loopState.Break();
                        }
                    }
                    catch (Exception e)
                    {
                        wrapper.Exception = e;
                        loopState.Break();
                    }
                    finally
                    {
                        wrapper.DurationMs = sw.ElapsedMilliseconds;
                    }
                });
            if (!state.LowestBreakIteration.HasValue)
                return null;

            var w = wrappers[Convert.ToInt32(state.LowestBreakIteration)];           
            if (w.Exception != null)
                throw new Exception("failed during parallel execution",w.Exception);
            else
                return w;
        }
    }
}
