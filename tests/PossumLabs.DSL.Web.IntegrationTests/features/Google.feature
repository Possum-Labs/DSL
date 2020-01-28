
Feature: Google

@scenarioAttribute
Scenario: google something
	Given navigated to 'http://google.com'
	When entering 'Possum Labs' into element 'Search'
	And clicking the element 'Google Search'
	Then the page contains the element 'About'

Scenario: google failed
	Given navigated to 'http://google.com'
	And an error is expected
	When clicking the element 'Bob'
	Then the Error has values
         | Message             |
         | /.*Google Search.*/ |
