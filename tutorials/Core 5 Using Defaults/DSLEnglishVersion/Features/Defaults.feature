Feature: Utilizing Defaults

Scenario: Explicit
	Given the User
		| var |
		| U1  |
	Given the Ticket
		| var | User |
		| T1  | U1   |
	Then 'T1.User.Id' has the value 'U1.Id' 

Scenario: Defaulted
	Given the Ticket
		| var |
		| T1  |
		| T2  |
	Then 'T1.User.Id' has the value 'T2.User.Id' 

Scenario: Login
	Given the User
		| var |
		| U1  |
	Given Logged in as User 'U1'

Scenario: Login explicit default
	Given Logged in as User 'default'

Scenario: Login implicit default
	Given Logged in as User ''

