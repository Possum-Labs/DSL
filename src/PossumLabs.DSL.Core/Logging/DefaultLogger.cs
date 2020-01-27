using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PossumLabs.DSL.Core.Logging
{
    public class DefaultLogger : ILog
    {
        public DefaultLogger(DirectoryInfo location, ILogFormatter logFormatter)
        {
            Location = location;
            LogFormatter = logFormatter;
        }

        protected virtual DirectoryInfo Location { get; }
        protected ILogFormatter LogFormatter { get; }

        public void File(string name, byte[] data, string extension="txt")
        {
            
            var file = new FileInfo($"{Location.FullName}/{name}.{extension}");
            if (!file.Exists) // you may not want to overwrite existing files
            {
                using (Stream stream = file.OpenWrite())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
            else
            {
                throw new Exception($"This file '{name}' already exists in the log location {Location.FullName}");
            }
            Write(LogFormatter.Format(null, new {
                Name = name,
                Extension = extension,
                FileName = file.RelativeFrom(Location) }));
        }

        public void Message(string message)
            =>Write(message);

        public void Section(string section, string content)
            =>Write(LogFormatter.Format(section, content));

        public void Section(string section, object content)
            => Write(LogFormatter.Format(section, content));

        virtual protected void Write(string message)
            => Console.WriteLine(message);
    }
}
