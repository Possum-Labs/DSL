using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.RepositoryDefaults
{
    public class Company : IValueObject
    {
    }

    public class CompanyRepository : RepositoryBase<Company>
    {
        public CompanyRepository(Interpeter interpeter, ObjectFactory objectFactory, TemplateManager templateManager) : base(interpeter, objectFactory, templateManager)
        {

        }
    }
}
