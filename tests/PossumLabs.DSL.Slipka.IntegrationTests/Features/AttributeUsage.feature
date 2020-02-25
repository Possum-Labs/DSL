Feature: Attribute Usage
	Make sure that recorded responses work correctly

@Slipka
Scenario: Using attributes to specify the host
	Given the Slipka Proxy
	| var| Destination           |
	| P1 | http://PossumLabs.com |
	And the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |

Scenario: Not Using attributes to specify the host
	Given the Slipka Proxy
	| var | Host                  | Destination           |
	| P1  | http://localhost:4445 | http://PossumLabs.com |
	And the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |