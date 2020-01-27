using PossumLabs.DSL.Core.Configuration;
using System;
using System.Collections.Generic;
using SkiaSharp;
using System.IO;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.Logging
{
    public class ImageLogging
    {
        public ImageLogging(ImageLoggingConfig config )
        {
            FontPercentage = config.SizePercentage;
            
            var fields = typeof(SKColors).GetFields();
            if (fields.Any(p => p.Name == config.Color))
                Color = (SKColor)fields.First(p => p.Name == config.Color).GetValue(null);
            else
                throw new GherkinException($"The Brush color of '{config.Color}' is invalid, please use one of these {fields.LogFormat(p => p.Name)}");
            if (config.SizePercentage < 0 || config.SizePercentage > 1)
                throw new GherkinException($"The sizePercentage of {config.SizePercentage} is invalid, please provide a value between 0 and 1");
        }

        private double FontPercentage { get; }
        private SKColor Color { get; }

        public byte[] AddTextToImage(byte[] image, string text)
        {
            var msIn = new MemoryStream(image);

            var img = SKBitmap.Decode(msIn);

            using (var canvas = new SKCanvas(img))
            {
                var pixelSize = Convert.ToSingle(img.Height * FontPercentage);

                var brush = new SKPaint
                {
                    Typeface = SKTypeface.Default,
                    TextSize = pixelSize,
                    IsAntialias = true,
                    Color = Color
                };

                canvas.DrawText(text, 0, 0, brush);

                canvas.Flush();

                var i = SKImage.FromBitmap(img);
                var data = i.Encode(SKEncodedImageFormat.Jpeg, 90);

                var msOut = new MemoryStream();
                data.SaveTo(msOut);
                return msOut.ToArray();
            }
        }
    }
}
