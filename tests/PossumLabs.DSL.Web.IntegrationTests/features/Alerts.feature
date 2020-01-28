@injected-html
Feature: Alerts

Scenario: Dismissing an alert
	Given injecting browser content
	| Html  | Script            |
	| empty | alert("bubbles"); |
	When dismissing the alert

Scenario: Accepting an confirm
	Given injecting browser content
	| Html  | Script                     |
	| empty | window.confirm("bubbles"); |
	When accepting the alert

Scenario: Dismissing an confirm
	Given injecting browser content
	| Html  | Script                     |
	| empty | window.confirm("bubbles"); |
	When dismissing the alert

Scenario: Check alert text
	Given injecting browser content
	| Html  | Script            |
	| empty | alert("bubbles"); |
	Then the alert has the value 'bubbles'
	When dismissing the alert

Scenario: Check confirm text
	Given injecting browser content
	| Html  | Script                     |
	| empty | window.confirm("bubbles"); |
	Then the alert has the value 'bubbles'
	When dismissing the alert


Scenario: Dismissing an alert from button click
	Given injecting browser content
	| Html                                               |
	| <button onclick="alert('bubbles')">target</button> |
	When clicking the element 'target'
	When dismissing the alert

Scenario: Accepting an confirm from button click
	Given injecting browser content
	| Html                                                        |
	| <button onclick="window.confirm('bubbles')">target</button> |
	When clicking the element 'target'
	When accepting the alert

Scenario: Dismissing an confirm from button click
	Given injecting browser content
	| Html                                                        |
	| <button onclick="window.confirm('bubbles')">target</button> |
	When clicking the element 'target'
	When dismissing the alert

Scenario: Check alert text from button click
	Given injecting browser content
	| Html                                               |
	| <button onclick="alert('bubbles')">target</button> |
	When clicking the element 'target'
	Then the alert has the value 'bubbles'
	When dismissing the alert

Scenario: Check confirm text from button click
	Given injecting browser content
	| Html                                                        |
	| <button onclick="window.confirm('bubbles')">target</button> |
	When clicking the element 'target'
	Then the alert has the value 'bubbles'
	When dismissing the alert