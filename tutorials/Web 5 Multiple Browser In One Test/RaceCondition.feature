Feature: Google

Scenario: last item race condition
This is to illustrate the structure
	Given the Sessions
		| var      |
		| session1 |
		| session2 |
	Given using Session 'session1'
	###
	#Add Last time to cart
	###
	Given using Session 'session2'
	###
	#Try and add Last time to cart
	#check out
	###
	Given using Session 'session1'
	###
	#check out
	###

Scenario: opening two different sites with auto redirects to https
this validates the functionality
	Given the Sessions
		| var      |
		| session1 |
		| session2 |
	Given using Session 'session1'
	Given navigated to 'http://google.com'
	Given using Session 'session2'
	Given navigated to 'http://bing.com'
	Then 'session1.Driver.Url' has the value '`https://google.com.*`'
	Then 'session2.Driver.Url' has the value '`https://www.bing.com/.*`'