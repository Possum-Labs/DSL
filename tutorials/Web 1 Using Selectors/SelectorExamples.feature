Feature: Selector Examples

Scenario Outline: google something
	Given navigated to 'http://google.com'
	When entering 'Possum Labs' into element '<selector>'
	And clicking the element 'Google Search'
	Then the page contains the element 'About'
Examples: 
	| name            | selector             |
	| by visible text | Search               |
	| by xpath        | //input[@name = "q"] |

Scenario Outline: bing something
	Given navigated to 'http://bing.com'
	When entering 'Possum Labs' into element '<selector>'
	And clicking the element 'Search the web'
	Then the page contains the element 'About'
Examples: 
	| name            | selector               |
	| by visible text | Enter your search term |
	| by id           | #sb_form_q             |
	| by class        | .search                |