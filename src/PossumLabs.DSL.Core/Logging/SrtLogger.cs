using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PossumLabs.DSL.Core.Logging
{
    public class SrtLogger
    {
        public SrtLogger(string name)
        {
            Messages = new List<Tuple<TimeSpan, string>>();
            Name = name;
        }
        private string Name { get; }

        public List<Tuple<TimeSpan, string>> Messages;

        public void AddMessage(TimeSpan stamp, string msg)
            => Messages.Add(new Tuple<TimeSpan, string>(stamp, msg));

        public string GetLogs()
        {
            var log = new StringBuilder();
            var oldTimespan = new TimeSpan();
            var index = 1;
            for(int i = 0; i< Messages.Count; i++)
            {
                log.AppendLine($"{index++}");
                log.AppendLine($"{getSrtTimestamp(oldTimespan)} --> {getSrtTimestamp(Messages[i].Item1)}");
                log.AppendLine($"{Messages[i].Item2}");
                log.AppendLine();
                oldTimespan = Messages[i].Item1;
            }

            return log.ToString();
        }

        private string getSrtTimestamp(TimeSpan ts)
        => $"{ts.Hours.ToString("D2")}:{ts.Minutes.ToString("D2")}:{ts.Seconds.ToString("D2")},{ts.Milliseconds.ToString("D3")}";
    }
}
