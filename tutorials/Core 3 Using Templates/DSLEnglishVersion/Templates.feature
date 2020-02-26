Feature: Utilizing Templates

Scenario: Using the Default template
	Given the User
		| var |
		| U1  |
	Then 'U1.Height' has the value '2'

Scenario: Existing Data Using the Default template
	Then 'Admin.Height' has the value '2'

Scenario: Using the Short template
	Given the User of type 'short'
		| var |
		| U1  |
	Then 'U1.Height' has the value '1'

Scenario: Existing Data  Using the Short template
	Then 'Guest.Height' has the value '1'

Scenario: Complex template example
	Given the Users of type 'tall'
		| var | Title              |
		| U1  | Benalish Hero      |
		| U2  | Roc of Kher Ridges |
	Given the User
		| var         | Title    |
		| UNoTemplate | U1.Title |
	Then 'U1.Height' has the value '3'
	Then 'U1.Title' has the value 'Benalish Hero'
	Then 'U2.Height' has the value '3'
	Then 'U2.Title' has the value 'Roc of Kher Ridges'
	Then 'UNoTemplate.Title' has the value 'U1.Title'
	#or
	Then 'UNoTemplate.Title' has the value 'Benalish Hero'