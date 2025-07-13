Feature: Sample

Scenario: Basic Login
	Given navigated to 'http://possumlabs.com/testsite/'
	When clicking the element 'Login'
	And entering 'possum' into element 'User Name'
	And entering 'possum' into element 'Password'
	And clicking the element 'Login'

	Then the page contains the element 'Add Dealer'

