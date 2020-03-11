Feature: Selector Examples

Scenario Outline: basic login
	Given navigated to 'http://possumlabs.com/testsite/'
	When entering 'possum' into element 'User Name'
	And entering 'possum' into element 'Password'
	And clicking the element '<selector>'

	Then the page contains the element 'Add Dealer'
Examples: 
	| name            | selector                |
	| by visible text | Login                   |
	| by xpath        | //input[@type="button"] |
	| by css selector | .btn-secondary          |
#	| by Id           | #login_button           |