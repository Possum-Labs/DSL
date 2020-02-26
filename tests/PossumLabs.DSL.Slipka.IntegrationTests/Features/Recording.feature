@Slipka
Feature: Recording

Background:
	Given the Slipka Proxy
	| var | Destination           |
	| P1  | http://PossumLabs.com |

Scenario: Recording happy path
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
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC[0]' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |

Scenario: record by method
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Method |
	| GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P2.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC[0]' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |

Scenario: record by method negative
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Method |
	| POST   |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P2.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC.Count' has the value '0'

	@ignore
Scenario: record by header

	@ignroe
Scenario: record by wildcard url

	@ignore
Scenario: record a large file


