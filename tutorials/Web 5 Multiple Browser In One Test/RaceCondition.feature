Feature: Google

Scenario: last item race condition
This is to illustrate the structure
	Given the Sessions
		| var      |
		| session1 |
		| session2 |
	Given the Session 'session1'
	###
	#Add Last item to cart
	###
	Given the Session 'session2'
	###
	#Try and add Last item to cart
	#check out
	###
	Given the Session 'session1'
	###
	#check out
	###

Scenario: opening two different sites with auto redirects to https
this validates the functionality
	Given the Sessions
		| var      |
		| session1 |
		| session2 |
	Given the Session 'session1'
	Given navigated to 'http://google.com'
	Given the Session 'session2'
	Given navigated to 'http://bing.com'
	Then 'session1.Driver.Url' has the value '/https://google.com.*/'
	Then 'session2.Driver.Url' has the value '/https://www.bing.com/.*/'