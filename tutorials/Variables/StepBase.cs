using BoDi;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Exceptions;
using PossumLabs.Specflow.Core.Files;
using PossumLabs.Specflow.Core.Logging;
using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Variables
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