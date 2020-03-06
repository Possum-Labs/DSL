Feature: Default Url

Scenario: using the default url from FrameworkInitializationSteps
	Given navigated to '/'
	When entering 'Possum Labs' into element 'Search'
	And clicking the element 'Google Search'
	Then the page contains the element 'About'
