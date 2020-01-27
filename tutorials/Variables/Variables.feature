Feature: Variables Initialialization

Scenario: Create an Employee
you can utilize the . notation to get to domain object properties. 
you can do so in arguments as well as in the creation of new objects in table format.
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	And the Employee
	| var | Name |
	| E2  | E1.Name  |
	Then 'E1.Name' has the value 'Bob'
	And 'E2.Name' has the value 'Bob'

Scenario: Heirarchy blank
when you leave the column off or don't populate it then it will set the default value.
	Given the Employee
	| var | Name | Reports |
	| E1  | Bob  |         |
	Then 'E1.Reports.Count' has the value '0'

Scenario: Heirarchy single
Basic indexers are supported but only one layer deep.
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Given the Employee
	| var | Name | Reports |
	| E2  | Mary | E1      |
	Then 'E2.Reports.Count' has the value '1'
	Then 'E2.Reports[0].Name' has the value 'Bob'

Scenario: Heirarchy multi
you can also specify mutliple items and it will turn it into a list behind the scenes.
	Given the Employees
	| var | Name | 
	| E1  | Bob  | 
	| E2  | Bob2 | 
	Given the Employee
	| var | Name | Reports |
	| E3  | Mary | E1, E2  |
	Then 'E3.Reports.Count' has the value '2'
	Then 'E3.Reports[1].Name' has the value 'Bob2'

Scenario: Json Deserialization
detected json is automatically deserialized
	Given the Employee
	| var | Name | Reports        |
	| E1  | Bob  | [{Name:"bob"}] |
	Then 'E1.Reports.Count' has the value '1'
	Then 'E1.Reports[0].Name' has the value 'bob'