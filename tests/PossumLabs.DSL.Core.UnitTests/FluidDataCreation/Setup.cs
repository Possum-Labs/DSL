using PossumLabs.DSL.Core.FluidDataCreation;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class Setup:SetupBase<Setup>
    {
        public Setup(
           IDataCreatorFactory dataCreatorFactory,
           ObjectFactory objectFactory,
           TemplateManager templateManager,
           Interpeter interpeter) : base(dataCreatorFactory, objectFactory, templateManager, interpeter)
        {

            ObjectFactory.Register<ParentObject>((f) =>
            {
                var i = new ParentObject();
                i.ComplexValue = ObjectFactory.CreateInstance<ValueObject>();
                return i;
            });

            ObjectFactory.Register<ChildObject>((f) =>
            {
                var i = new ChildObject();
                i.ComplexValue = ObjectFactory.CreateInstance<ValueObject>();
                return i;
            });

            ParentObjects = new RepositoryBase<ParentObject>(Interpeter, ObjectFactory);
            ChildObjects = new RepositoryBase<ChildObject>(Interpeter, ObjectFactory);

            Interpeter.Register(ParentObjects);
            Interpeter.Register(ChildObjects);
        }

        public RepositoryBase<ParentObject> ParentObjects { get; }
        public RepositoryBase<ChildObject> ChildObjects { get; }

        [WithCreator("ParentObjects")]
        public Setup WithParentObject(string name, string template = null, Action<ParentObjectSetup> configurer = null)
            => With<ParentObject,ParentObjectSetup,int>(ParentObjects, name, template, configurer);

        [WithCreator("ParentObjects")]
        public Setup WithParentObjects(int count, string template = null, Action<ParentObjectSetup> configurer = null)
            => WithMany<ParentObject, ParentObjectSetup, int>(ParentObjects, WithParentObject, count, template, configurer);

        [WithCreator("ChildObjects")]
        public Setup WithChildObject(string name, string template = null, Action<ChildObjectSetup> configurer = null)
            => With<ChildObject, ChildObjectSetup, int>(ChildObjects, name, template, configurer);

        [WithCreator("ChildObjects")]
        public Setup WithChildObjects(int count, string template = null, Action<ChildObjectSetup> configurer = null)
            => WithMany<ChildObject, ChildObjectSetup, int>(ChildObjects, WithChildObject, count, template, configurer);
    }
}
