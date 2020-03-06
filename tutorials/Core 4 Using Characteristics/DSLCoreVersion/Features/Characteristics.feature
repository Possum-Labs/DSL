Feature: Utilizing Characteristics

Scenario: Using the Default template
	Given the User
		| var |
		| U1  |
	Given the User 'U1' is locked-out
	Then 'U1.IslocakedOut' has the value 'True' 

Scenario: Existing Data Using the Default template
	Given the User that is 'locked out'
		| var |
		| U1  |
	Then 'U1.IslocakedOut' has the value 'True' 

