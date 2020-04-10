using PossumLabs.DSL.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;
using AnimatedGif;
using System.IO;

namespace PossumLabs.DSL.Web.Diagnostic
{
    public class ScreenshotProcessor : IScreenshotProcessor
    {
        public void CreateGif(string fileName, IEnumerable<byte[]> files)
        {
            using (var gif = AnimatedGif.AnimatedGif.Create(fileName, 1000))
            {
                foreach (var file in files)
                {
                    var ms = new MemoryStream(file);
                    gif.AddFrame(System.Drawing.Image.FromStream(ms));
                }
            }
        }
    }
}
