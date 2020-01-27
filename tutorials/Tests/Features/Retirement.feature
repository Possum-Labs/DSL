Feature: Retirement

Scenario: Simple
	Given the Employee
	| var | Name | 
	| E1  | Bob  | 
	And the Employee
	| var | Name | Reports |
	| E2  | Mary | E1      |
	And the root Employee is 'E2'
	When Employee 'E1' Retires
	Then 'E2.Role' has the value 'CEO'
	And 'E2.Reports' has the value '[]'

Scenario: Promotion
	Given the Employee
	| var | Name | 
	| E1  | Bob  | 
	And the Employee
	| var | Name | Reports |
	| E2  | Mary | E1      |
	And the root Employee is 'E2'
	When Employee 'E2' Retires
	Then 'E1.Role' has the value 'CEO'
	And 'E1.Reports' has the value '[]'

Scenario: multiple reports
	Given the Employees
	| var | Name | Seniority |
	| E1  | Bob1 | 1         |
	| E2  | Bob2 | 2         |
	And the Employee
	| var | Name | Reports |
	| E3  | Mary | E1, E2  |
	And the root Employee is 'E3'
	When Employee 'E2' Retires
	Then 'E3.Role' has the value 'CEO'
	Then 'E3.Reports' contains the values
	| Name    |
	| E1.Name |
	And 'E1.Role' has the value 'Minion'
	And 'E1.Reports' has the value '[]'
	And 'E2.Role' has the value 'null'
	And 'E2.Reports' has the value '[]'

Scenario: John needs proof
	Given the Employees
	| var | Name | Seniority |
	| E1  | Bob  | 1         |
	| E2  | Jan  | 1         |
	And the Employees
         | var | Name | Reports |
         | E3  | Mary | E1      |
         | E4  | Joe  | E2      |
	And the Employee
         | var | Name | Reports |
         | E5  | Tom  | E3, E4  |
	And the root Employee is 'E5'
	When Employee 'E3' Retires
	Then 'E5.Reports' contains the values
	| Name    |
	| E1.Name |
	And 'E1.Role' has the value 'Minion'