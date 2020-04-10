using System;
using System.IO;

namespace PossumLabs.DSL.Core.Files
{
    public interface IFileManager
    {
        void Initialize(string featureName, string scenarioName, string example = null);
        Uri PersistFile(byte[] file, string exactName);
        Uri PersistFile(byte[] file, string type, string extention);
        Uri PersistFile(IFile file, string type, string extention);
        Uri PersistFile(Stream file, string exactName);
        Uri PersistFile(Stream file, string type, string extention);
        Uri PersistFile(string content, string exactName);
    }
}