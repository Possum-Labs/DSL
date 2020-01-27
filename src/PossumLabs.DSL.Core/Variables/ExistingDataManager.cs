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
    public class ExistingDataManager
    {
        public ExistingDataManager(Interpeter interpeter)
        {
            Interpeter = interpeter;
        }

        private Interpeter Interpeter { get; }
        
        public void Initialize(Assembly assembly)
        {
            var fileInfo = new FileInfo(assembly.Location);
            var directoryInfo = fileInfo.Directory;
            var types = GetAllTypesOf<IEntity>(assembly).Where(t => !t.IsAbstract && !t.IsInterface).ToList();
            var simplenames = types.Select(t => t.Name);

            var files = GetAllFiles(directoryInfo, "json")
                .Where(f=> string.Equals(f.Name, "existing.json", StringComparison.InvariantCultureIgnoreCase));

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

                        var expectedMembers = type.GetValueMembers().Where(m => !types.Contains(m.Type));

                        foreach (var keyValue in objectSet.values)
                        {
                            Interpeter.Add(type, (keyValue.key ?? keyValue.Key).Value, ProcessVariable(file, type, expectedMembers, (keyValue.value ?? keyValue.Value)));
                        }
                    }
                }
            }
        }

        private object ProcessVariable(FileInfo file, Type type, IEnumerable<ValueMemberInfo> expectedMembers, dynamic options)
        {
            var o = Activator.CreateInstance(type);
            string name = options.Name ?? options.name;

            dynamic template = options.Template ?? options.template ?? options;

            var existingMembers = new List<string>();
            foreach (var line in template)
                existingMembers.Add(line.Name);

            var dtypeProps = template.GetType().GetProperties();
            foreach (var member in expectedMembers)
            {
                var dynamicMemberName = existingMembers.FirstOrDefault(m => String.Equals(m, member.Name, StringComparison.CurrentCultureIgnoreCase));
                if (dynamicMemberName == null)
                    continue;
                member.SetValue(o, template[dynamicMemberName].Value);
            }
            return o;
        }

        private IEnumerable<FileInfo> GetAllFiles(DirectoryInfo directoryInfo, string extension)
            => directoryInfo.GetFiles().Where(f => f.Extension == $".{extension}").Union(directoryInfo.GetDirectories().SelectMany(d => GetAllFiles(d, extension)));


        private IEnumerable<Type> GetAllTypesOf<T>( Assembly assembly)
            =>assembly.GetExportedTypes()
                .Union(assembly.GetReferencedAssemblies().Select(Assembly.Load).Select(a => a.GetExportedTypes())
                .SelectMany(ts => ts)
                .Where(t => typeof(T).IsAssignableFrom(t)));
    }
}
