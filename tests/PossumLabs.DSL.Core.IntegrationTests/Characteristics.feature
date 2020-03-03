Feature: Characteristics

Scenario: no template
	Given the Test Object
		| var |
		| TO  |
	Then 'TO.IsSpecial' has the value 'False'
	And 'TO.Created' has the value 'True'

Scenario: just template
	Given the Test Object that is 'special'
		| var |
		| TO  |
	Then 'TO.TemplateName' has the value 'default'
	And 'TO.Created' has the value 'True'
	And 'TO.IsSpecial' has the value 'True'

Scenario: pecific template
	Given the Test Object of type 'templateA' that is 'special'
		| var |
		| TO  |
	Then 'TO.TemplateName' has the value 'A'
	And 'TO.Created' has the value 'True'
	And 'TO.IsSpecial' has the value 'True'

