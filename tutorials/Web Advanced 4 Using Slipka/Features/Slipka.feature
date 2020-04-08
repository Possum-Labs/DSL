Feature: Slipka

Scenario: Using Slipka Explicit
	Given using a Proxy
	And configure Proxy to look for reports
	And navigated to 'home'
	And clicking the element 'download'
	When retrieving the file from proxy as 'F1'

@report
@proxy
Scenario: Using Slipka
	Given navigated to 'home'
	And clicking the element 'download'
	When retrieving the file from proxy as 'F1'
