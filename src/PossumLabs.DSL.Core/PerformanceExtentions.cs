using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public static class PerformanceExtentions
    {
        private static O Time<O>(Func<O> f, string name)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return f();
            }
            finally
            {
                Trace.WriteLine($"lambda in {name} : {sw.ElapsedMilliseconds} ms");
            }
        }

        private static void Time(Action a, string name)
        => Time<int>(() => { a(); return 42; }, name);

        //(new Action(() => Thread.Sleep(100)).Instrument())();
        public static Action Instrument(this Action a, [CallerMemberName] string caller = null)
        => () => Time(a, caller);

        public static Action<T> Instrument<T>(this Action<T> a, [CallerMemberName] string caller = null)
        => (t) => Time(()=>a(t), caller);

        public static Action<T1, T2> Instrument<T1, T2>(this Action<T1, T2> a, [CallerMemberName] string caller = null)
        => (t1, t2) => Time(()=>a(t1, t2), caller);

        public static Action<T1, T2, T3> Instrument<T1, T2, T3>(this Action<T1, T2, T3> a, [CallerMemberName] string caller = null)
        => (t1, t2, t3) => Time(()=>a(t1, t2, t3), caller);

        public static Func<T> Instrument<T>(this Func<T> f, [CallerMemberName] string caller = null)
        => () => Time<T>(f, caller);
     
        public static Func<I,O> Instrument<I, O>(this Func<I,O> f, [CallerMemberName] string caller = null)
        => (i1) => Time<O>(()=>f(i1), caller);

        public static Func<I1, I2, O> Instrument<I1, I2, O>(this Func<I1, I2, O> f, [CallerMemberName] string caller = null)
        => (i1, i2) => Time<O>(() => f(i1, i2), caller);

        public static Func<I1, I2, I3, O> Instrument<I1, I2, I3, O>(this Func<I1, I2, I3, O> f, [CallerMemberName] string caller = null)
        => (i1, i2, i3) => Time<O>(() => f(i1, i2, i3), caller);
       
        public static Func<I1, I2, I3, I4, O> Instrument<I1, I2, I3, I4, O>(this Func<I1, I2, I3, I4, O> f, [CallerMemberName] string caller = null)
        => (i1, i2, i3, i4) => Time<O>(() => f(i1, i2, i3, i4), caller);
    }
}
