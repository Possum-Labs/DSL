@Slipka
Feature: Decoration

Background:
	Given the Slipka Proxy
	| var | Destination           |
	| P1  | http://PossumLabs.com |
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |
	Given the Slipka Proxy
	| var | Destination |
	| P3  | P2.ProxyUri |

Scenario: Decorations happy path on response
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Method |
	| GET    |
	Given the Proxy 'P3' decorates with
	| Key        | Values            |
	| my-header  | ['Test']          |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P3.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC[0].Request.Headers' contains the values
	| Key        | Values            |
	| my-header  | ['Test']          |

Scenario: Decorations multiple on response
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Method |
	| GET    |
	Given the Proxy 'P3' decorates with
	| Key        | Values    |
	| my-header  | ['Test']  |
	| my-header1 | ['Test1'] |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P3.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC[0].Request.Headers' contains the values
	| Key        | Values    |
	| my-header  | ['Test']  |
	| my-header1 | ['Test1'] |

Scenario: Decorations composit on response inline
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Method |
	| GET    |
	Given the Proxy 'P3' decorates with
	| Key        | Values            |
	| my-headers | ['Test1','Test2'] |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P3.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC[0].Request.Headers' contains the values
	| Key        | Values           |
	| my-headers | ['Test1, Test2'] |

Scenario: Decorations composit on response split line
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	Given the Proxy 'P2' records the calls
	| Method |
	| GET    |
	Given the Proxy 'P3' decorates with
	| Key        | Values    |
	| my-headers | ['Test1'] |
	| my-headers | ['Test2'] |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P3.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	And wait 1000 ms
	Then retrieving the recorded calls from Proxy 'P2' as 'RC'
	And 'RC[0].Request.Headers' contains the values
	| Key        | Values           |
	| my-headers | ['Test1, Test2'] |

Scenario: Decorations happy path on forwarding
Scenario: Decorations multiple on forwarding
Scenario: Decorations composit on forwarding