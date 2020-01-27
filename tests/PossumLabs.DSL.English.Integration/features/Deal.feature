Feature: Deal

Scenario: Create Deal
	Given logged in as User 'Admin'
	When clicking the element 'Add deal'
	And entering 'Bob' into element 'Contact person name'
	And entering 'Possum Labs' into element 'Organization name'
	And entering 'Testing 123' into element 'Deal title'
	And entering '42' into element 'Deal value'
	And entering '1/1/2000' into element 'Expected close date'
	And clicking the element 'Save'
	And clicking the element 'Testing 123'

Scenario:  Create Deal Shortcut
	Given logged in as User 'Admin'
	Given the Deal
	| var |
	| D1  |
	When clicking the element 'D1.Title'

Scenario:  Consolodate companies
	Given logged in as User 'Admin'
	Given the Deal
	| var |
	| D1  |
	And the Deal
	| var | OrganizationName    |
	| D2  | D1.OrganizationName |
	When clicking  the element 'D1.Title'
	And clicking  the element 'D1.OrganizationName'
	Then the page contains the element 'D1.ContactPersonName'
	And the page contains the element 'D2.ContactPersonName'

Scenario:  Create Won Deal Shortcut
	Given logged in as User 'Admin'
	Given the Deal that is 'Won'
	| var |
	| D1  |


Scenario:  Create Lost Deal Shortcut
	Given logged in as User 'Admin'
	Given the Deal that is 'Lost'
	| var |
	| D1  |

