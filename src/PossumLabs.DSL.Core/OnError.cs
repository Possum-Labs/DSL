using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public static class OnError
    {
        public static void Continue(Action a)
        {
            a.OnErrorContinue();
        }
    }
}
