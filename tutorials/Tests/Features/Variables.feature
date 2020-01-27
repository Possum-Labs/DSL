Feature: Variables

Scenario: Create an Employee
	Given the Employee
	| var | Name |
	| E1  | Bob  |

Scenario: Heirarchy blank
	Given the Employee
	| var | Name | Reports |
	| E1  | Bob  |         |

Scenario: Heirarchy null
	Given the Employees
	| var | Name | Reports |
	| E1  | Bob  | null    |

Scenario: Heirarchy single
	Given the Employee
	| var | Name |
	| E1  | Bob  |
	Given the Employee
	| var | Name | Reports |
	| E2  | Mary | E1      |

Scenario: Heirarchy multi
	Given the Employees
	| var | Name | 
	| E1  | Bob  | 
	| E2  | Bob2 | 
	Given the Employee
	| var | Name | Reports |
	| E3  | Mary | E1, E2  |

