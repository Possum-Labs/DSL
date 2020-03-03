Feature: Template
	
Scenario: default template
	Given the Test Object
		| var |
		| TO  |
	Then 'TO.TemplateName' has the value 'default'

Scenario: pecific template
	Given the Test Object of type 'templateA'
		| var |
		| TO  |
	Then 'TO.TemplateName' has the value 'A'

