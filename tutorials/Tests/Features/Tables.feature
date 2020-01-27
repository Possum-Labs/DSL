@SingleBrowser @injected-html
Feature: Table Selectors

Scenario Outline: Entering data into table
	Given injecting browser content
	| Table   |
	| <table> |
	When entering into Table
	|          | Col1    |
	| <target> | <value> |
	Then the Table has values
	|          | Col1    |
	| <target> | <value> |
Examples: 
	| description          | target | value | table                                                                                   |
	| th simple text input | target | Bob   | <tr><th>Key</th><th>Col1</th></tr><tr><td>target</td><td><input type="text"/></td></tr> |
	| td simple text input | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td>target</td><td><input type="text"/></td></tr> |

Scenario Outline: Finding propper cells in tables
	Given injecting browser content
	| Table   |
	| <table> |
	When entering into Table
	| Key      | Col1    |
	| <target> | <value> |
	Then the Table has values
	| Key      | Col1    |
	| <target> | <value> |
Examples: 
	| description          | target | value | table                                                                                                              |
	| th simple text input | target | Bob   | <tr><th>Key</th><th>Col1</th></tr><tr><td>target</td><td><input type="text"/></td></tr>                            |
	| th noise text input  | target | Bob   | <tr><th>stuff</th><th>Key</th><th>Col1</th></tr><tr><td>junk</td><td>target</td><td><input type="text"/></td></tr> |

Scenario: Failed match
	Given injecting browser content
	| Table                                                                                   |
	| <tr><th>Key</th><th>Col1</th></tr><tr><td>target</td><td><input type="text"/></td></tr> |
	When entering into Table
	| Key    | Col1 |
	| target | Bob  |
	Given an error is expected
	Then the Table has values
	| Key    | Col1  |
	| target | Marry |
	Then the Error has values
	| Message                                    |
	| /the value was 'Bob' wich was not 'Marry'/ |