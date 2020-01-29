using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Files;

namespace PossumLabs.DSL.Core.Logging
{
    public class MovieLogger
    {
        public MovieLogger(
            FileManager fileManager,
            MovieLoggerConfig movieLoggerConfig, 
            ScenarioMetadata scenarioMetadata)
        {
            FileManager = fileManager;
            ScenarioMetadata = scenarioMetadata;
            MovieLoggerConfig = movieLoggerConfig;

            Stopwatch = Stopwatch.StartNew();
            StepLogger = new SrtLogger("step");
            OtherLogger = new SrtLogger("other");
            Images = new List<Tuple<TimeSpan, byte[]>>();
        }

        private FileManager FileManager { get; }
        private SrtLogger StepLogger { get; }
        private SrtLogger OtherLogger { get; }
        private Stopwatch Stopwatch { get; }
        private List<Tuple<TimeSpan, byte[]>> Images { get; }
        private ScenarioMetadata ScenarioMetadata { get; }
        private MovieLoggerConfig MovieLoggerConfig { get; }
        public bool IsEnabled { get; set; }

        public void StepEnd(string step)
            => StepLogger.AddMessage(Stopwatch.Elapsed, step);


        public void AddScreenShot(byte[] image)
        {
            lock (Images)
            {
                Images.Add(new Tuple<TimeSpan, byte[]>(Stopwatch.Elapsed, image));
            }
        }

        private string BuildFfmpegFilter()
        {
            //zoompan=d=25+'50*eq(in,3)'+'100*eq(in,5)'

            var builder = new StringBuilder();

            builder.Append($"zoompan=d=0");
            var oldTimespan = new TimeSpan();
            lock (Images)
            {
                for (int i = 0; i < Images.Count; i++)
                {
                    var current = Images[i].Item1;
                    builder.Append($"+'{Convert.ToInt32(25 * (current.TotalSeconds - oldTimespan.TotalSeconds))}*eq(in,{i + 1})'");
                    oldTimespan = current;
                }
            }
            return builder.ToString();
        }
        private string BuildMetadataFile()
        {
            var log = new StringBuilder();
            var oldTimespan = new TimeSpan();

            //https://ffmpeg.org/ffmpeg-formats.html#Metadata-1
            // Metadata keys or values containing special characters(‘=’, ‘;’, ‘#’, ‘\’ and a newline) must be escaped with a backslash ‘\’.

            Func<string, string> metadataEncode = (input) => input?
                        .Replace(@"\",@"\\")
                        .Replace(@"\n",@"\\n")
                        .Replace("=", @"\=")
                        .Replace(";", @"\;")
                        .Replace("#", @"\#");

            ///
            /// ;FFMETADATA1
            /// title = Sample scenario
            /// ; this is a comment
            /// artist = Envision
            /// title=
            /// description=
            log.AppendLine($";FFMETADATA1");
            log.AppendLine($"; This is a generated metadata file");
            log.AppendLine($"title={metadataEncode(ScenarioMetadata.ScenarioName)}");
            log.AppendLine($"description=Feature:{metadataEncode(ScenarioMetadata.FeatureName)} \\");
            log.AppendLine($"Scenario:{metadataEncode(ScenarioMetadata.ScenarioName)} \\");
            log.AppendLine($"Created:{metadataEncode(DateTime.Now.ToString())} \\");
            foreach(var item in ScenarioMetadata.MetaData)
                log.AppendLine($"{metadataEncode(item.Key)}:{metadataEncode(item.Value)} \\");

            log.AppendLine();
            for (int i = 0; i < StepLogger.Messages.Count; i++)
            {
                var curret = StepLogger.Messages[i].Item1;
                ///
                /// [CHAPTER]
                /// TIMEBASE = 1 / 1000
                /// START = 0
                /// END = 5000
                /// title = Intro
                log.AppendLine($"[CHAPTER]");
                log.AppendLine($"TIMEBASE=1/1000");
                log.AppendLine($"START={Convert.ToUInt32(oldTimespan.TotalMilliseconds)}");
                log.AppendLine($"END={Convert.ToUInt32(curret.TotalMilliseconds)}");
                log.AppendLine($"title={metadataEncode(StepLogger.Messages[i].Item2)}");
                log.AppendLine();
                oldTimespan = curret;
            }
            ///
            /// [STREAM]
            /// title = Sample scenario
            log.AppendLine($"[STREAM]");
            log.AppendLine($"title={metadataEncode(ScenarioMetadata.ScenarioName)}");

            return log.ToString();
        }

        public void ComposeMovie()
        {
            
            string tempDirName = Guid.NewGuid().ToString().Replace("-","");
            var parentDir = new DirectoryInfo(System.Environment.CurrentDirectory);
            //create folder
            var workingDir = parentDir.CreateSubdirectory(tempDirName);

            FileManager.PersistFile(BuildFfmpegFilter(), "filtering");
            FileManager.PersistFile(BuildMetadataFile(), "metadata");
            FileManager.PersistFile(StepLogger.GetLogs(), "step.srt");
            FileManager.PersistFile(OtherLogger.GetLogs(), "debug.srt");

            var index = 0;

            //create files
            lock (Images)
            {
                foreach (var file in Images)
                {
                    var filename = "img" + index.ToString("D4") + ".png";
                    FileManager.PersistFile(file.Item2, filename);
                    index++;
                }
            }

            //create create images

            var encodeArguments =
                $"{MovieLoggerConfig.PathToFfmpeg} -y -i img%4d.png -s:v {MovieLoggerConfig.Resolution} -r {MovieLoggerConfig.FrameRate}" +
                $" -crf 1 -filter_complex_script filtering test.mp4";

            var chapterArguments = $"{MovieLoggerConfig.PathToFfmpeg} -y -i test.mp4 -i metadata -map_metadata 1 -codec copy test-new.mp4";

            var subtitleArguments = $"{MovieLoggerConfig.PathToFfmpeg}  -y -i test-new.mp4 -i step.srt -i debug.srt " +
                 $"-map 0:v -map 1 -map 2 " +
                 $"-c:v copy -c:a copy -c:s srt " +
                 $"-metadata:s:s:0 language=eng -metadata:s:s:1 language=epo " +
                 $"test.mkv";

            subtitleArguments = $"{MovieLoggerConfig.PathToFfmpeg}  -y -i test-new.mp4 -i step.srt  " +
                 $"-map 0:v -map 1  " +
                 $"-c:v copy -c:a copy -c:s srt " +
                 $"-metadata:s:s:0 language=eng " +
                 $"test.mkv";

            var sb = new StringBuilder();
            sb.AppendLine(encodeArguments);
            sb.AppendLine(chapterArguments);
            sb.AppendLine(subtitleArguments);

            FileManager.PersistFile(sb.ToString(), "moviefy.ps1");
        }


    }
}
