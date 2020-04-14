Feature: Slipka

Scenario: Using Slipka Explicit
	Given using a Proxy
	And configure Proxy to look for reports
	And navigated to 'testsite'
	When clicking the element 'Login'
	And entering 'possum' into element 'User Name'
	And entering 'possum' into element 'Password'
	And clicking the element 'Login'
	Given navigated to 'assets/Reports/Logo.pdf'
	When sleep for '5' seconds
	When retrieving the file from proxy as 'F1'

@report
@proxy
Scenario: Using Slipka
	Given navigated to 'testsite'
	When clicking the element 'Login'
	And entering 'possum' into element 'User Name'
	And entering 'possum' into element 'Password'
	And clicking the element 'Login'
	Given navigated to 'assets/Reports/Logo.pdf'
	When sleep for '5' seconds
	When retrieving the file from proxy as 'F1'
