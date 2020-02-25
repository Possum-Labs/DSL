@Slipka
Feature: Tagging Calls

Background:
	Given the Slipka Proxy
	| var | Destination           |
	| P1  | http://PossumLabs.com |

Scenario: Tagging happy path
	Given the Proxy 'P1' injects the calls
	| Uri   | Response Content | StatusCode | Method |
	| test  | Hello World      | 200        | GET    |
	| other | Hello World      | 200        | GET    |
	Given the Proxy 'P1' tags the calls
	| Uri  | Tags     |
	| test | ['Test'] |
	And the Call
	| var | Host        | Path  | Method |
	| C1  | P1.ProxyUri | test  | GET    |
	| C2  | P1.ProxyUri | other | GET    |
	When the Call 'C1' is executed
	And the Call 'C2' is executed
	And wait 1000 ms
	Then retrieving the tagged calls from Proxy 'P1' with tag 'Test' as 'RC'
	And 'RC' has the values
	| Count |
	| 1     |
	And 'RC[0]' has the values
	| Path  | StatusCode | Tags     |
	| /test | 200        | ['Test'] |
	And retrieving the Session from Proxy 'P1' as 'S1'
	And 'S1' has the values
	| Tags     |
	| ['Test'] |


Scenario: Tagging slow calls
	Given the Proxy 'P1' injects the calls
	| Uri  | Response Content | StatusCode | Method | Duration |
	| fast | Hello World      | 200        | GET    | 1        |
	| slow | Hello World      | 200        | GET    | 4000     |
	Given the Slipka Proxy
	| var | Destination |
	| P2  | P1.ProxyUri |
	Given the Proxy 'P2' tags the calls
	| Tags     | Method | Duration |
	| ['Test'] | GET    | 3000     |
	And the Call
	| var | Host        | Path | Method |
	| C1  | P2.ProxyUri | fast | GET    |
	| C2  | P2.ProxyUri | slow | GET    |
	When the Call 'C1' is executed
	And the Call 'C2' is executed
	And wait 1000 ms
	Then retrieving the tagged calls from Proxy 'P2' with tag 'Test' as 'RC'
	And 'RC' has the values
	| Count |
	| 1     |
	And 'RC[0]' has the values
	| Path  | StatusCode | Tags     |
	| /slow | 200        | ['Test'] |
	And retrieving the Session from Proxy 'P2' as 'S2'
	And 'S2' has the values
	| Tags     |
	| ['Test'] |