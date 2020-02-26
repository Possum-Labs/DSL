using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.RepositoryDefaults
{
    [TestClass]
    public class DefaultTests
    {
        public ObjectFactory ObjectFactory { get; set; }
        public Interpeter Interpeter { get; set; }
        public TemplateManager TemplateManager { get; set; }
        public SubDivisionRepository SubDivisionRepository { get; set; }
        public DivisionRepository DivisionRepository { get; set; }
        public CompanyRepository CompanyRepository { get; set; }

        public Company Company { get; set; }
        public int CompanyCount { get; set; }
        public Division Division { get; set; }
        public int DivisionCount { get; set; }
        public SubDivision SubDivision { get; set; }
        public int SubDivisionCount { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
            TemplateManager = new TemplateManager();

            Company = new Company();
            Division = new Division();
            SubDivision = new SubDivision();

            ObjectFactory.Register<Company>(x => { 
                CompanyCount++; 
                return Company; });
            CompanyRepository = new CompanyRepository(Interpeter, ObjectFactory, TemplateManager);
            CompanyRepository.InitializeCharacteristicsTransition(
                (x) => x,
                PossumLabs.DSL.Core.Variables.Characteristics.None);
            Interpeter.Register(CompanyRepository);

            ObjectFactory.Register<Division>(x => { 
                DivisionCount++; 
                return Division; });
            DivisionRepository = new DivisionRepository(Interpeter, ObjectFactory, TemplateManager);
            DivisionRepository.InitializeCharacteristicsTransition(
                 (x) => x,
                PossumLabs.DSL.Core.Variables.Characteristics.None);
            Interpeter.Register(DivisionRepository);

            ObjectFactory.Register<SubDivision>(x => { 
                SubDivisionCount++; 
                return SubDivision; });
            SubDivisionRepository = new SubDivisionRepository(Interpeter, ObjectFactory, TemplateManager);
            SubDivisionRepository.InitializeCharacteristicsTransition(
                 (x) => x,
                PossumLabs.DSL.Core.Variables.Characteristics.None);
            Interpeter.Register(SubDivisionRepository);
        }

        [TestMethod]
        public void NothingToDo()
        {
            var ret = CompanyRepository.GetDefault();
            ret.Should().Be(Company);
            CompanyCount.Should().Be(1);
            DivisionCount.Should().Be(0);
            SubDivisionCount.Should().Be(0);
        }

        [TestMethod]
        public void OneLayer()
        {
            var ret = DivisionRepository.GetDefault();
            ret.Should().Be(Division);
            CompanyCount.Should().Be(1);
            DivisionCount.Should().Be(1);
            SubDivisionCount.Should().Be(0);
        }

        [TestMethod]
        public void MultiLayer()
        {
            var ret = SubDivisionRepository.GetDefault();
            ret.Should().Be(SubDivision);
            CompanyCount.Should().Be(1);
            DivisionCount.Should().Be(1);
            SubDivisionCount.Should().Be(1);
        }

        [TestMethod]
        public void PreventRepeatCalls()
        {
            var ret1 = new SubDivision();
            SubDivisionRepository.DecorateNewItem(ret1);
            var ret2 = new SubDivision();
            SubDivisionRepository.DecorateNewItem(ret2);
            CompanyCount.Should().Be(1);
            DivisionCount.Should().Be(1);
            SubDivisionCount.Should().Be(0);
        }

        [TestMethod]
        public void LeaveInitializedProperties()
        {
            var ret1 = new SubDivision();
            ret1.Division = new Division();
            SubDivisionRepository.DecorateNewItem(ret1);
            CompanyCount.Should().Be(0);
            DivisionCount.Should().Be(0);
            SubDivisionCount.Should().Be(0);
        }
    }
}
