@injected-html @ignore
Feature: Logs
I don't have a solution for this at the moment

Scenario: Simple Log Message
	Given injecting browser content
	| Html                                                     |
	| <button onclick="console.log('bubbles')">target</button> |
	When clicking the element 'target'
	Then the Browser Logs has the value 'bubbles'

	Scenario: Exception Log Message
	Given injecting browser content
	| Html                                              |
	| <button onclick="throw 'bubbles'">target</button> |
	When clicking the element 'target'
	Then the Browser Logs has the value 'bubbles'