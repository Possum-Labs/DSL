using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Logging
{
    public interface ILog
    {
        void Section(string section, object content);
        void Section(string section, string content);
        void File(string type, byte[] data, string extension = "txt");
        void Message(string message);
    }
}
