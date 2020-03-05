
Feature: Google

Scenario: google something
	Given navigated to 'http://google.com'
	When entering 'Possum Labs' into element 'Search'
	And clicking the element 'Google Search'
	Then the page contains the element 'About'


