using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Core.IntegrationTests
{
    public class TestObject : IEntity
    {
        public TestObject()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string AString { get; set; }
        public int AInt { get; set; }
        public long ALong { get; set; }
        public float AFloat { get; set; }
        public decimal ADecimal { get; set; }
        public bool ABool { get; set; }
        public bool Created { get; internal set; }
        public bool IsSpecial { get; internal set; }
        public string TemplateName { get; set; }
        public string ExistingName { get; set; }

        public string LogFormat()
            => $"id:{Id}";
    }

    [Binding]
    public class TestObjectRepositorySteps : RepositoryStepBase<TestObject>
    {
        public TestObjectRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        { 
            Repository.InitializeDefault(() =>
            {
                var testObject = new TestObject();
                CreateTestObject(testObject);
                return testObject;
            });
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateTestObject(x);
                return x;
            }, Characteristics.None);
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateTestObject(x);
                MakeSpecial(x);
                return x;
            }, "special");
        }

        [Given(@"the Test Objects?")]
        public void GivenThetestObjects(Dictionary<string, TestObject> testObjects)
            => GivenThetestObjects(null, Characteristics.None, testObjects);

        [Given(@"the Test Objects? of type '([\w ]*)'")]
        public void GivenThetestObjects(string template, Dictionary<string, TestObject> testObjects)
            => GivenThetestObjects(template, Characteristics.None, testObjects);

        [Given(@"the Test Objects? that (?:is|are) '([\w ,]*)'")]
        public void GivenThetestObjects(Characteristics characteristics, Dictionary<string, TestObject> testObjects)
            => GivenThetestObjects(null, characteristics, testObjects);

        [Given(@"the Test Objects? of type '([\w ]*)' that (?:is|are) '(.*)'")]
        public void GivenThetestObjects(
            string template = null, 
            Characteristics characteristics = null, 
            Dictionary<string, TestObject> testObjects = null)
        {
            foreach (var testObject in testObjects.Values)
                TemplateManager.ApplyTemplate(testObject, template);
            foreach (var testObject in testObjects.Values)
                base.Repository.CharacteristicsTransitionMethods[characteristics](testObject);
            foreach (var key in testObjects.Keys)
                Add(key, testObjects[key]);
        }

        private void CreateTestObject(TestObject testObject) 
        {
            testObject.Created = true;
        }
        private void MakeSpecial(TestObject testObject) 
        {
            testObject.IsSpecial = true;
        }

    }
}
