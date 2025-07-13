using Reqnroll.BoDi;
using PossumLabs.DSL.DataGeneration;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using Reqnroll;

namespace PossumLabs.DSL
{
    public abstract class StepsBase
    {
        public StepsBase(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;

        }

        protected IObjectContainer ObjectContainer { get; }
        protected ScenarioContext ScenarioContext { get => ObjectContainer.Resolve<ScenarioContext>(); }
        protected FeatureContext FeatureContext { get => ObjectContainer.Resolve<FeatureContext>(); }

        protected IInterpeter Interpeter => ObjectContainer.Resolve<IInterpeter>();
        protected IActionExecutor Executor => ObjectContainer.Resolve<IActionExecutor>();
        protected ILog Log => ObjectContainer.Resolve<ILog>();
        protected IObjectFactory ObjectFactory => ObjectContainer.Resolve<IObjectFactory>();
        protected ITemplateManager TemplateManager => ObjectContainer.Resolve<ITemplateManager>();
        protected IFileManager FileManager => ObjectContainer.Resolve<IFileManager>();
        protected DataGenerator DataGenerator => ObjectContainer.Resolve<DataGenerator>();

        protected ScenarioMetadata Metadata => ObjectContainer.Resolve<ScenarioMetadata>();

        protected void Register<T>(T item) where T : class
            => ObjectContainer.RegisterInstanceAs<T>(item, dispose: true);
    }
}