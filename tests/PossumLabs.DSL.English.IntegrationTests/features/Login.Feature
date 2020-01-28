Feature: Login

Scenario: Hardcoded Login
	Given navigated to 'https://possumlabs.pipedrive.com/'
	When entering 'pipedrive@possumlabs.com' into element 'Email'
	And entering 'nbojSPYy@84m' into element 'Password'
	And clicking the element 'Log in'
	Then the page contains the element 'Add deal'


Scenario: Config Driven Login
	Given navigated to 'https://possumlabs.pipedrive.com/'
	When entering 'Admin.Email' into element 'Email'
	And entering 'Admin.Password' into element 'Password'
	And clicking the element 'Log in'
	Then the page contains the element 'Add deal'

Scenario: Shortcut in Login
	Given logged in as User 'Admin'
