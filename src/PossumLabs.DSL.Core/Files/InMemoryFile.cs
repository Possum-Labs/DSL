using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PossumLabs.DSL.Core.Files
{
    public class InMemoryFile : IFile, IOnErrorNotified
    {
        public InMemoryFile(byte[] data, string type = "txt")
        {
            Data = data;
            Path = "In Memory";
        }

        private byte[] Data { get; }
        private string Path { get; set; }
        private string Type { get; }

        public virtual Stream Stream { get => new MemoryStream(Data); }

        public void OnError(string name, ILog log)
            =>log.File(name, Data);

        public string LogFormat()
            => Path;
    }
}
