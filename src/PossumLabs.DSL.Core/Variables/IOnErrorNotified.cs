using PossumLabs.DSL.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public interface IOnErrorNotified
    {
        void OnError(string name, ILog log);
    }
}
