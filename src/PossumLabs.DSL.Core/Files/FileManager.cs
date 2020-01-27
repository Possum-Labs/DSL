using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PossumLabs.DSL.Core.Files
{
    public class FileManager
    {
        public FileManager(DatetimeManager datetimeManager)
        {
            Start = datetimeManager.Now();
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            ConfigFodler = config["logFolder"] ?? "logs";

            if(!Path.IsPathRooted(ConfigFodler))
                ConfigFodler = Path.Combine(
                        new FileInfo(this.GetType().Assembly.Location).DirectoryName,
                        ConfigFodler);

            Order = 1;
        }

        public void Initialize(string featureName, string scenarioName, string example = null)
        {
            FeatureName = featureName;
            ScenarioName = scenarioName;
            ExampleName = example;

            ConfigFodler = Path.Combine(
                ConfigFodler,
                $"{FeatureName}-{ScenarioName}-{ExampleName}-{Start.ToString("yyyyMMdd_HHmmss")}"
                    // < (less than)
                    .Replace('<', ' ')
                    // > (greater than)
                    .Replace('>', ' ')
                    // : (colon - sometimes works, but is actually NTFS Alternate Data Streams)
                    .Replace(':', ' ')
                    // " (double quote)
                    .Replace('"', ' ')
                    // / (forward slash)
                    .Replace('/', ' ')
                    // \ (backslash)
                    .Replace('\\', ' ')
                    // | (vertical bar or pipe)
                    .Replace('|', ' ')
                    // ? (question mark)
                    .Replace('?', ' ')
                    // * (asterisk)
                    .Replace('*', ' '));

            BaseFolder = new DirectoryInfo(ConfigFodler);

            if (!BaseFolder.Exists)
                BaseFolder.Create();
        }
        private DateTime Start { get; }
        private string ConfigFodler { get; set; }
        private string FeatureName { get; set; }
        private string ScenarioName { get; set; }
        private string ExampleName { get; set; }
        private DirectoryInfo BaseFolder { get; set; }

        private int Order { get; set; }

        private string GetFileName(string type, string extension)
            => $"{type}-{Order++}.{extension}"
            // < (less than)
            .Replace('<', ' ')
            // > (greater than)
            .Replace('>', ' ')
            // : (colon - sometimes works, but is actually NTFS Alternate Data Streams)
            .Replace(':', ' ')
            // " (double quote)
            .Replace('"', ' ')
            // / (forward slash)
            .Replace('/', ' ')
            // \ (backslash)
            .Replace('\\', ' ')
            // | (vertical bar or pipe)
            .Replace('|', ' ')
            // ? (question mark)
            .Replace('?', ' ')
            // * (asterisk)
            .Replace('*', ' ');

        public Uri PersistFile(IFile file, string type, string extention)
            => PersistFile(file.Stream, type, extention);

        public Uri PersistFile(byte[] file, string type, string extention)
            => PersistFile(new MemoryStream(file), type, extention);

        public Uri PersistFile(Stream file, string type, string extention)
            => PersistFile(file, GetFileName(type, extention));

        public Uri PersistFile(string content, string exactName)
            => PersistFile(Encoding.ASCII.GetBytes(content), exactName);

        public Uri PersistFile(byte[] file, string exactName)
            => PersistFile(new MemoryStream(file), exactName);

        public Uri PersistFile(Stream file, string exactName)
        {
            var info = new FileInfo(Path.Combine(BaseFolder.FullName, exactName));
            var w = info.Create();
            file.CopyToAsync(w).ContinueWith((x) => w.Close());
            return new Uri(info.FullName);
        }
    }
}
