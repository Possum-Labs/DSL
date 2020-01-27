using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.PossumLab
{
    public interface IProvideDebugInformation
    {
        IEnumerable<DebugInformationProvider> Providers { get; }
    }

    public class DebugInformationProvider
    {
        public DebugInformationProvider()
        {
            Id = $"{Guid.NewGuid()}";
        }
        public string Name { get; set; }
        public string DisplayProvider { get; set; }
        public Func<byte[]> Screen { get; set; }
        public Func<TypeView> Object { get; set; }
        public Func<string> Text { get; set; }
        public Func<string> Json { get; set; }
        public string Id { get; set; }

        public static string Jpeg => "Jpeg";
        public static string Gif => "Gif";
        public static string TypeView => "TypeView";
        public static string String => "String";
    }
}
