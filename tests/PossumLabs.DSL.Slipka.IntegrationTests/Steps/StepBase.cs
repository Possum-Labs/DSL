using Reqnroll.BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    public abstract class StepBase
    {
        public StepBase(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;

        }

        protected IObjectContainer ObjectContainer { get; }
        protected ScenarioContext ScenarioContext { get => ObjectContainer.Resolve<ScenarioContext>(); }
        protected FeatureContext FeatureContext { get => ObjectContainer.Resolve<FeatureContext>(); }


        protected Interpeter Interpeter => ObjectContainer.Resolve<Interpeter>();
        protected ActionExecutor Executor => ObjectContainer.Resolve<ActionExecutor>();
        protected ILog Log => ObjectContainer.Resolve<ILog>();
        protected ObjectFactory ObjectFactory => ObjectContainer.Resolve<ObjectFactory>();
        protected TemplateManager TemplateManager => ObjectContainer.Resolve<TemplateManager>();
        protected FileManager FileManager => ObjectContainer.Resolve<FileManager>();

        protected ScenarioMetadata Metadata => ObjectContainer.Resolve<ScenarioMetadata>();




        internal void Register<T>(T item) where T : class
            => ObjectContainer.RegisterInstanceAs<T>(item, dispose: true);
    }
}