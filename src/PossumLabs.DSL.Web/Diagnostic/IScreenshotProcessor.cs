using System.Collections.Generic;

namespace PossumLabs.DSL.Web.Diagnostic
{
    public interface IScreenshotProcessor
    {
        void CreateGif(string fileName, IEnumerable<byte[]> files);
    }
}