@Slipka
Feature: OrderViolations

Background:
	Given the Slipka Proxy
	| var | Destination           |
	| P1  | http://PossumLabs.com |
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Given an error is expected

Scenario: Trying to add Tagging after the first call
	Given the Proxy 'P1' tags the calls
	| Uri   | Tags     |
	| /test | ['Test'] |
	Then the Error has values
	| Message  |
	| Conflict |

Scenario: Trying to add Injection after the first call
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Then the Error has values
	| Message  |
	| Conflict |

Scenario: Trying to add Recording after the first call
	Given the Proxy 'P1' records the calls
	| Uri   | 
	| /test | 
	Then the Error has values
	| Message  |
	| Conflict |

Scenario: Trying to add Decoration after the first call
	Given the Proxy 'P1' decorates with
	| Key       | Values   |
	| my-header | ['Test'] |
	Then the Error has values
	| Message  |
	| Conflict |
