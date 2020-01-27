using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Linq;

namespace PossumLabs.DSL.DataGeneration
{
    public class DataStoresManager
    {
        public List<DataStore> LoadEmbeddedResources()
        {
            var ret = new List<DataStore>();
            var assembly = typeof(DataStoresManager).Assembly;
            var resourceNames = assembly.GetManifestResourceNames();

            var resources = new List<string>
                {
                    DataTypes.Creatures,
                    DataTypes.FemaleFirstNames,
                    DataTypes.LastNames,
                    DataTypes.MaleFirstNames,
                    DataTypes.Seeds,
                };
            foreach(var resourceName in resources)
            {
                var ds = new DataStore(resourceName);
                ret.Add(ds);
                var name = resourceNames.First(x => x.Contains(resourceName));
                using (Stream stream = assembly.GetManifestResourceStream(name))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        ds.Initialize(content);
                    }
                }
            }
            return ret;
        }
    }
}
