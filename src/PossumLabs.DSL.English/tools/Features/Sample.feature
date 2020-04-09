Feature: Sample

Scenario: Basic Login
	Given navigated to 'http://possumlabs.com/testsite/'
	When clicking the element 'Login'
	And entering 'possum' into element 'User Name'
	And entering 'possum' into element 'Password'
	And clicking the element 'Login'

	Then the page contains the element 'Add Dealer'

Scenario: Existing data
	Given navigated to 'http://possumlabs.com/testsite/'
	When clicking the element 'Login'
	And entering 'Admin.Username' into element 'User Name'
	And entering 'Admin.Password' into element 'Password'
	And clicking the element 'Login'

	Then the page contains the element 'Add Dealer'

Scenario: Existing data with shortcut
	Given Logged in as User 'Admin'

	Then the page contains the element 'Add Dealer'

Scenario: Explicit defaults with shortcut
	Given Logged in as User 'default'

	Then the page contains the element 'Add Dealer'

Scenario: Implicit defaults with shortcut
	Given Logged in as User ''

	Then the page contains the element 'Add Dealer'

Scenario: New Data for VIP user
	Given the User
	| var | Username | Password | IsVip |
	| U1  | possum   | possum   | true  |
	And Logged in as User 'U1'

	Then the page contains the element 'Add Dealer'

Scenario: New Data for VIP user using templates
	Given the User of type 'VIP'
	| var | Username | Password |
	| U1  | possum   | possum   |
	And Logged in as User 'U1'

	Then the page contains the element 'Add Dealer'
	And 'U1.IsVip' has the value 'True'

Scenario: New Data for VIP user using templates and characteristics
	Given the User of type 'VIP' that is 'logged in'
	| var | Username | Password |
	| U1  | possum   | possum   |

	Then the page contains the element 'Add Dealer'
	And 'U1.IsVip' has the value 'True'

Scenario: Existing data using templates
	Then 'Admin.IsVip' has the value 'True'