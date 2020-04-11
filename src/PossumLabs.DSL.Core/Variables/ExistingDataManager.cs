using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class ExistingDataManager : IExistingDataManager
    {
        public ExistingDataManager(IInterpeter interpeter, ITemplateManager templateManager)
        {
            Interpeter = interpeter;
            TemplateManager = templateManager;
        }

        private IInterpeter Interpeter { get; }
        private ITemplateManager TemplateManager { get; }

        public void Initialize(Assembly assembly)
        {
            var fileInfo = new FileInfo(assembly.Location);
            var directoryInfo = fileInfo.Directory;
            var types = GetAllTypesOf<IEntity>(assembly).Where(t => !t.IsAbstract && !t.IsInterface).ToList();
            var simplenames = types.Select(t => t.Name);

            var files = GetAllFiles(directoryInfo, "json")
                .Where(f => string.Equals(f.Name, "existing.json", StringComparison.InvariantCultureIgnoreCase));

            foreach (var file in files)
            {
                using (StreamReader r = new StreamReader(file.OpenRead()))
                {
                    string json = r.ReadToEnd();
                    dynamic obj = JValue.Parse(json);

                    foreach (var objectSet in obj)
                    {
                        var type = types.FirstOrDefault(t => t.Name == (objectSet.type ?? objectSet.Type).Value);
                        if (type == null)
                            throw new Exception(
                                $"unable to find {(objectSet.type ?? objectSet.Type).Value} " +
                                $"did find {simplenames.LogFormat()} " +
                                $"for assembly {assembly.FullName} " +
                                $"in folder {directoryInfo.FullName}");



                        foreach (var keyValue in objectSet.values)
                        {
                            Interpeter.Add(
                                type,
                                (keyValue.var ?? keyValue.Var).Value,
                                ProcessVariable(
                                    type,
                                    (keyValue.var ?? keyValue.Var).Value,
                                    (keyValue.template ?? keyValue.Template)?.Value,
                                    (keyValue.value ?? keyValue.Value)));
                        }
                    }
                }
            }
        }

        public object ProcessVariable(
            Type type,
            string name,
            string template,
            dynamic options)
        {
            var expectedMembers = type.GetValueMembers();

            var o = Activator.CreateInstance(type);

            //apply the template
            TemplateManager.ApplyTemplate(type, o, template);

            //apply the existing.json
            var existingMembers = new List<string>();

            foreach (var line in options)
                existingMembers.Add(line.Name);

            foreach (var member in expectedMembers)
            {
                var dynamicMemberName = existingMembers.FirstOrDefault(m => String.Equals(m, member.Name, StringComparison.CurrentCultureIgnoreCase));
                if (dynamicMemberName == null)
                    continue;
                member.SetValue(o, Interpeter.Cast(options[dynamicMemberName].Value, member.Type));
            }

            //override using environment variables
            foreach (var valueMember in expectedMembers)
            {
                var envVar = Environment.GetEnvironmentVariable($"{name}_{valueMember.Name}");
                if (envVar == null)
                    continue;
                valueMember.SetValue(o, Interpeter.Convert(valueMember.Type, envVar));
            }
            return o;
        }

        private IEnumerable<FileInfo> GetAllFiles(DirectoryInfo directoryInfo, string extension)
            => directoryInfo.GetFiles().Where(f => f.Extension == $".{extension}").Union(directoryInfo.GetDirectories().SelectMany(d => GetAllFiles(d, extension)));


        private IEnumerable<Type> GetAllTypesOf<T>(Assembly assembly)
            => assembly.GetExportedTypes()
                .Union(assembly.GetReferencedAssemblies().Select(Assembly.Load).Select(a => a.GetExportedTypes())
                .SelectMany(ts => ts)
                .Where(t => typeof(T).IsAssignableFrom(t)));
    }
}
