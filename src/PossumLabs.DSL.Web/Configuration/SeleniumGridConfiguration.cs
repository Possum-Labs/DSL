using Microsoft.Extensions.Configuration;
using PossumLabs.DSL.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PossumLabs.DSL.Web.Configuration
{
    public class SeleniumGridConfiguration
    {

        public SeleniumGridConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();
            Url = config["seleniumGridUrl"];
            if(!int.TryParse(config["seleniumRetryMs"], out var retry))
                new GherkinException($"Can't parse seleniumRetryMs, found value {config["seleniumRetryMs"]} this has to be an integer like 10000 for 10 seconds");
            RetryMs = retry;
            Width = int.Parse(config["seleniumWidth"]??"1280");
            Height = int.Parse(config["seleniumHeight"]??"720");
            Username = config["seleniumGridUsername"];
            AccessKey = config["seleniumGridAccessKey"];
        }

        public string Url { get; set; }
        public int RetryMs { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Username { get; set; }
        public string AccessKey { get; set; }
    }
}
