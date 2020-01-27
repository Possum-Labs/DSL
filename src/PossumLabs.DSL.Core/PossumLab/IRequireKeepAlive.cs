using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.PossumLab
{
    public interface IRequireKeepAlive
    {
        int KeepAlivePeriodInMs { get; }
        Action KeepAlive { get; }
    }
}
