@Slipka
Feature: Simulate

Background:
	Given the Slipka Proxy
	| var | Destination           |
	| P1  | http://PossumLabs.com |

Scenario: Simulate response happy path
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |

	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Uri   |
	| /test |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P2.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the response of call '0' for Proxy 'P2' as File 'F1'
	And the File 'F1' has the content 'Hello World'

Scenario: Simulate request happy path
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |

	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Bad              | 200        | POST   |
	Given the Proxy 'P2' records the calls
	| Uri   |
	| /test |
	And the Call
	| var | Host        | Path | Method | Request Content |
	| C1  | P2.ProxyUri | test | POST   | Hello World     |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the request of call '0' for Proxy 'P2' as File 'F1'
	And the File 'F1' has the content 'Hello World'