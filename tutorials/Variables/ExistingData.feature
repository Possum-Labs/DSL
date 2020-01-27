Feature: ExistingData
take a look at existing.json as well as the SetupExistingData method in FrameworkInitializationSteps
this functionality is intended to handeling scenarios where you have some seed data, or golden data 
in your system. You can have data for any type that has a repository registered. 

Scenario: check existing user data
	Then 'DemoEmployee.Name' has the value 'bob'

