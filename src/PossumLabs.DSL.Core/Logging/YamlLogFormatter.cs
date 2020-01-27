using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PossumLabs.DSL.Core.Logging
{
	public class YamlLogFormatter : ILogFormatter
	{
		public YamlLogFormatter()
		{
			Serializer = new SerializerBuilder()
				.WithNamingConvention(CamelCaseNamingConvention.Instance)
				.Build();
		}

		public ISerializer Serializer { get; set; }

		public string Format(string section, object data)
		{
			var result = new StringBuilder();
			var temp = new { Section = section, Data = data } ;
			result.AppendLine("--yaml data begin--");
			result.Append(Serializer.Serialize(temp));
			result.AppendLine("--yaml data end--");
			return result.ToString();
		}
	}
}