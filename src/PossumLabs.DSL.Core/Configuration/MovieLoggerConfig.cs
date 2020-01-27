using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Configuration
{
    [ConfigurationObject("MovieLogger")]
    public class MovieLoggerConfig
    {
        public MovieLoggerConfig()
        {
            PathToFfmpeg = @"C:\FFMPEG\ffmpeg.exe ";
            Resolution = "1920x1080";
            FrameRate = 25;
        }

        [ConfigurationMember("PathToFfmpeg")]
        public string PathToFfmpeg { get; set; }
        [ConfigurationMember("Resolution")]
        public string Resolution { get; set; }
        [ConfigurationMember("FrameRate")]
        public int FrameRate { get; set; }
    }
}
