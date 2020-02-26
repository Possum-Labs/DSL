@Slipka
Feature: Expiration

Scenario: Making sure Auto close works
	Given the Slipka Proxy
	| var | Destination           | OpenFor  |
	| P1  | http://PossumLabs.com | 00:00:05 |
	And the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	And wait 10000 ms
	Given an error is expected
	When the Call 'C1' is executed
	Then the Error has values
	| Message                                |
	| `The underlying connection was closed` |

Scenario: Making sure Auto archiving works
	Given the Slipka Proxy
	| var | Destination           | RetainedFor |
	| P1  | http://PossumLabs.com | 00:00:05    |
	And the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then close the Proxy 'P1'
	And wait 60000 ms
	Given an error is expected
	Then retrieving the recorded calls from Proxy 'P1' as 'RC'
	Then the Error has values
	| Message                 |
	| `Value cannot be null.` |

