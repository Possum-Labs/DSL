using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class TemplateManager
    {
        public TemplateManager()
        {
            Templates = new Dictionary<Type, Dictionary<string, Action<object>>>();
            DefaultKey = Guid.NewGuid().ToString();
        }

        private Dictionary<Type, Dictionary<string, Action<object>>> Templates { get; }
        private string DefaultKey { get; }

        public void Register<T>(Action<T> template, string name = null) where T:IValueObject
        {
            var t = typeof(T);
            Register(name, t, (o)=>template((T)o));
        }

        private void Register(string name, Type t, Action<object> template)
        {
            if (!Templates.ContainsKey(t))
                Templates.Add(t, new Dictionary<string, Action<object>>());
            Templates[t].Add(name?? DefaultKey, template);
        }

        public T ApplyTemplate<T>(T item, string name = null) where T : IValueObject
            => (T)ApplyTemplate(typeof(T), item, name);
        public object ApplyTemplate(Type t, object item, string name = null)
        {
            if (!Templates.ContainsKey(t) && name == null)
                return item;
            if (!Templates.ContainsKey(t) && name != null)
                throw new Exception($"There are no templates registered for {t.Name}. The template {name} does not exist for type {t.Name}");
            if (!Templates[t].ContainsKey(name?? DefaultKey) && name != null)
                throw new GherkinException($"The template {name??"null"} fores not exist for type {t.Name}, registered templates are {Templates[t].Keys.LogFormat()}");
            if (!Templates.ContainsKey(t) || !Templates[t].ContainsKey(name?? DefaultKey))
                return item;
            Templates[t][name?? DefaultKey](item);
            return item;
        }

        public void Initialize(Assembly assembly)
        {
            var fileInfo = new FileInfo(assembly.Location);
            var directoryInfo = fileInfo.Directory;
            var dlls = GetAllFiles(directoryInfo, "dll").Select(f=>f.Name).ToList();
            var types = GetAllTypesOf<IEntity>(dlls, assembly).Where(t=>!t.IsAbstract && !t.IsInterface).ToList();
            var simplenames = types.Select(t => t.Name);

            var files = GetAllFiles(directoryInfo, "json").Where(f => simplenames.Contains(Path.GetFileNameWithoutExtension(f.Name)));

            foreach(var file in files)
            {
                var type = types.First(t => t.Name == Path.GetFileNameWithoutExtension(file.Name));
                var expectedMembers = type.GetValueMembers().Where(m=>!types.Contains(m.Type));
                using (StreamReader r = new StreamReader(file.OpenRead()))
                {
                    string json = r.ReadToEnd();
                    dynamic obj = JValue.Parse(json);
                    if(obj is IEnumerable)
                    {
                        foreach(var o in obj)
                            ProcessTemplate(file, type, expectedMembers, o);
                    }
                    else
                    {
                        ProcessTemplate(file, type, expectedMembers, obj);
                    }
                }
            }
        }

        private void ProcessTemplate(FileInfo file, Type type, IEnumerable<ValueMemberInfo> expectedMembers, dynamic options)
        {
            string name = options.Name??options.name;

            dynamic template = options.Template ?? options.template ?? options;

            var existingMembers = new List<string>();
            foreach (var line in template)
                existingMembers.Add(line.Name);

            void action(object o)
            {
                var dtypeProps = template.GetType().GetProperties();
                foreach (var member in expectedMembers)
                {
                    var dynamicMemberName = existingMembers.FirstOrDefault(m => String.Equals(m, member.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (dynamicMemberName == null)
                        continue;
                    var token = template[dynamicMemberName];
                    if (token is JArray)
                    {
                        var a = (JArray)token;
                        var l = (IList)member.GetValue(o);
                        var listType = l.GetType().GetGenericArguments()[0];
                        foreach (var i in a.Cast<dynamic>())
                        {
                            l.Add(((object)i.Value).TryConvertTo(listType));
                        }
                    }
                    else
                    {
                        member.SetValue(o, template[dynamicMemberName].Value);
                    }
                }
            };

            Register(name, type, action);
        }

        private IEnumerable<FileInfo> GetAllFiles(DirectoryInfo directoryInfo, string extension)
            => directoryInfo.GetFiles().Where(f=>f.Extension == $".{extension}").Union(directoryInfo.GetDirectories().SelectMany(d=>GetAllFiles(d, extension)));

        private IEnumerable<Type> GetAllTypesOf<T>(List<string> availableDlls, Assembly assembly)
        {
            var platform = Environment.OSVersion.Platform.ToString();
          

            return assembly.GetExportedTypes().Union(assembly.GetReferencedAssemblies().Select(Assembly.Load).Select(a=>a.GetExportedTypes()).SelectMany(ts=>ts)
                .Where(t => typeof(T).IsAssignableFrom(t)));
        }
    }
}
