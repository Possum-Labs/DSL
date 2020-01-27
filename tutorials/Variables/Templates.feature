Feature: Templates
Take a look at Employee.json this defines 2 templates, one that focuses on the default scenario and sets the role
This is similar to initializing in the constructor of an object, except that it can be maintained outside of code.

The second template, Contractor, creates a custom value for the Role.

Any properties can be set on your objects uing this method. 

Scenario: default template
	Given the Employee
	| var |
	| E1  |
	Then 'E1.Role' has the value 'Default'

Scenario: specific template
	Given the Employee of type 'Contractor'
	| var |
	| E1  |
	Then 'E1.Role' has the value 'Contractor'


Scenario: overriding template
	Given the Employee of type 'Contractor'
	| var |
	| E1  |
	And setting the properties
	| var | Role       |
	| E1  | Consultant |
	Then 'E1.Role' has the value 'Consultant'