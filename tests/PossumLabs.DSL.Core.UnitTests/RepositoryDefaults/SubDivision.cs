using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.RepositoryDefaults
{
    public class SubDivision : IValueObject
    {
        [NullCoalesceWithDefault]
        public Division Division { get; set; }
    }

    public class SubDivisionRepository : RepositoryBase<SubDivision>
    {
        public SubDivisionRepository(Interpeter interpeter, ObjectFactory objectFactory, TemplateManager templateManager) : base(interpeter, objectFactory, templateManager)
        {

        }
    }
}
