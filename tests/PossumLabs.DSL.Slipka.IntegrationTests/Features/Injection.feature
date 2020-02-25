@Slipka
Feature: Injection
	Make sure that recorded responses work correctly

Background:
	Given the Slipka Proxy
	| var | Destination           |
	| P1  | http://PossumLabs.com |

Scenario: Get Hello World
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |

Scenario: Error response
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | my error         | 500        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| my error         | 500        |

Scenario Outline: Other methods
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method   |
	| /test | <Body>           | 200        | <Method> |
	And the Call
	| var | Host        | Path | Method   |
	| C1  | P1.ProxyUri | test | <Method> |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| <ResponseContent>           | 200        |
Examples: 
| Method  | Body | ResponseContent | Description                                          |
| GET     | null |                 | Requests data from a specified resource              |
| POST    | {}   | "{}"            | Submits data to be processed to a specified resource |
| PUT     | {}   | "{}"            | Uploads a representation of the specified URI        |
| DELETE  | null |                 | Deletes the specified resource                       |
| OPTIONS | null |                 | Returns the HTTP methods that the server supports    |

Scenario: Delay
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method | Duration |
	| /test | Hello World      | 200        | GET    | 1000     |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Duration |
	| > 1000   |


Scenario: Make sure that we don't forward the original call
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |
	Given the Proxy 'P2' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| /test | Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P2.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then retrieving the calls from Proxy 'P1' as 'RC' 
	And 'RC' has the values
	| Count |
	| 0     |

Scenario: inject by header

Scenario: inject by method
	Given the Proxy 'P1' injects the calls
	| Response Content | StatusCode | Method |
	| Hello World      | 200        | GET    |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |

Scenario: inject by regular expression in path
	Given the Proxy 'P1' injects the calls
	| Uri      | Response Content | StatusCode |
	| /t[est]+ | Hello World      | 200        |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P1.ProxyUri | test | GET    |
	When the Call 'C1' is executed
	Then 'C1' has the values
	| Response Content | StatusCode |
	| Hello World      | 200        |

Scenario: inject by regular expression in header
