using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.RepositoryDefaults
{
    public class Division : IValueObject
    {
        [DefaultToRepositoryDefault]
        public Company Company { get; set; }
    }

    public class DivisionRepository : RepositoryBase<Division>
    {
        public DivisionRepository(Interpeter interpeter, ObjectFactory objectFactory, TemplateManager templateManager) : base(interpeter, objectFactory, templateManager)
        {

        }
    }
}
