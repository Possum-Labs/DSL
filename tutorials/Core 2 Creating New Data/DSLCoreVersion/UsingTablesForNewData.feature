Feature: UsingTablesForNewData

Scenario: Complex table input example
	Given the Users
		| var | Title              |
		| U1  | Benalish Hero      |
		| U2  | Roc of Kher Ridges |
	Given the User
		| var             | Title    |
		| UDuplicateTitle | U1.Title |
	Then 'U1.Title' has the value 'Benalish Hero'
	Then 'U2.Title' has the value 'Roc of Kher Ridges'
	Then 'UDuplicateTitle.Title' has the value 'U1.Title'
	#or
	Then 'UDuplicateTitle.Title' has the value 'Benalish Hero'