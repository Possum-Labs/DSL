using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.DataGeneration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PossumLabs.DSL.DataGeneration
{
    public class DataGeneratorRepository : RepositoryBase<DataGenerator>
    {
        public DataGeneratorRepository(Interpeter interpeter, ObjectFactory objectFactory) : base(interpeter, objectFactory)
        {
            DataStoresManager = new DataStoresManager();
            DataStores = new Lazy<List<DataStore>>(() => DataStoresManager.LoadEmbeddedResources());
        }

        private DataStoresManager DataStoresManager { get; }
        private Lazy<List<DataStore>> DataStores { get; }

        public DataGenerator BuildGenerator()
        {
            var stores = DataStores.Value;
            DataGenerator.GenerateCreatures = stores.First(x => x.Name == DataTypes.Creatures);
            DataGenerator.GenerateFemaleFirstNames = stores.First(x => x.Name == DataTypes.FemaleFirstNames);
            DataGenerator.GenerateLastNames = stores.First(x => x.Name == DataTypes.LastNames);
            DataGenerator.GenerateMaleFirstNames = stores.First(x => x.Name == DataTypes.MaleFirstNames);
            return new DataGenerator()
            {
                Creatures = stores.First(x=>x.Name == DataTypes.Creatures),
                FemaleFirstNames = stores.First(x => x.Name == DataTypes.FemaleFirstNames),
                LastNames = stores.First(x => x.Name == DataTypes.LastNames),
                MaleFirstNames = stores.First(x => x.Name == DataTypes.MaleFirstNames)
            };
        }
    }
}
