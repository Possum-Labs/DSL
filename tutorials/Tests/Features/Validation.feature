Feature: validation

Scenario: field validation
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Then 'E1.Name' has the value 'Bob'

Scenario: field validation regular expression
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Then 'E1.Name' has the value '/B.*/'

Scenario: Contains single
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Given the Employee
	| var | Name | Reports |
	| E2  | Mary | E1      |
	Then 'E2.Reports' contains the values
	| Name    |
	| E1.Name |

Scenario: Contains single regular expression
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Given the Employee
	| var | Name | Reports |
	| E2  | Mary | E1      |
	Then 'E2.Reports' contains the values
	| Name  |
	| /B.*/ |

Scenario:contains Multi
	Given the Employees
	| var | Name | 
	| E1  | Bob  | 
	| E2  | Bob2 | 
	Given the Employee
	| var | Name | Reports |
	| E3  | Mary | E1, E2  |
	Then 'E3.Reports' contains the values
	| Name    |
	| E1.Name |
	| E2.Name |

Scenario: Failed match
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Given an error is expected
	Then 'E1.Name' has the value 'Marry'
	Then the Error has values
	| Message                                    |
	| /the value was 'Bob' wich was not 'Marry'/ |

Scenario: Failed contain Contain single
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Given an error is expected
	Then 'E1.Reports' contains the values
	| Name    |
	| E1.Name |
	Then the Error has values
	| Message                       |
	| Unable to find all the values |