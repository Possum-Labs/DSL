Feature: Templates

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