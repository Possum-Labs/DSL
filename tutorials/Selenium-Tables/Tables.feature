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


Scenario Outline: Key heirarchy
	Given injecting browser content
	| Table   |
	| <table> |
	Then the Table has values
	|          | Col1    |
	| <target> | <value> |
Examples: 
	| description      | target | value | table                                                                                                                                          |
	| First column     | target | Bob   | <tr><td>Key 1</td><td>Key 2</td><td>Col1</td></tr><tr><td>target</td><td></td><td>Bob</td></tr>                                                |
	| Second column    | target | Bob   | <tr><td>Key 1</td><td>Key 2</td><td>Col1</td></tr><tr><td></td><td>target</td><td>Bob</td></tr>                                                |
	| Prioritize First | target | Bob   | <tr><td>Key 1</td><td>Key 2</td><td>Col1</td></tr><tr><td></td><td>target</td><td>Wrong</td></tr><tr><td>target</td><td></td><td>Bob</td></tr> |


Scenario Outline: specify key column
	Given injecting browser content
	| Table   |
	| <table> |
	Then the Table has values
	| Key      | Col1    |
	| <target> | <value> |
Examples: 
	| description | target | value | table                                                                                                                                        |
	| Simple      | target | Bob   | <tr><td>Key</td><td>other</td><td>Col1</td></tr><tr><td></td><td>target</td><td>Wrong</td></tr><tr><td>target</td><td></td><td>Bob</td></tr> |
	| not first   | target | Bob   | <tr><td>other</td><td>Key</td><td>Col1</td></tr><tr><td></td><td>target</td><td>Bob</td></tr><tr><td>target</td><td></td><td>Wrong</td></tr> |


Scenario Outline: Entering data into table with hidden inputs
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
	| description   | target | value | table                                                                                                                       |
	| before target | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td>target<input type="hidden"/></td><td><input type="text"></td></tr>                |
	| after target  | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td><input type="hidden"/>target</td><td><input type="text"></td></tr>                |
	| before label  | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td><label>target</label><input type="hidden"/></td><td><input type="text"></td></tr> |
	| after label   | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td><input type="hidden"/><label>target</label></td><td><input type="text"></td></tr> |
	| before input  | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td>target</td><td><input type="hidden"><input type="text"/></td></tr>                |
	| between lines | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><input type="hidden"><tr><td>target</td><td><input type="text"/></td></tr>                |

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
	| description                   | target | value | table                                                                                                                                                                  |
	| th simple text input          | target | Bob   | <tr><th>Key</th><th>Col1</th></tr><tr><td>target</td><td><input type="text"/></td></tr>                                                                                |
	| th noise text input           | target | Bob   | <tr><th>stuff</th><th>Key</th><th>Col1</th></tr><tr><td>junk</td><td>target</td><td><input type="text"/</td></tr>                                                      |
	| make sure columns are skipped | target | Bob   | <tr><th>Other</th><th>Key</th><th>Col1</th></tr><tr><td>target</td><td>ignore</td><td>Bad</td></tr><tr><td>ignore</td><td>target</td><td><input type="text"/</td></tr> |

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
	| Message                                     |
	| /the value was 'Bob' which was not 'Marry'/ |

Scenario Outline: Failed validations
	Given injecting browser content
	| Table   |
	| <table> |
	Given an error is expected
	Then the Table has values
	|          | Col1    |
	| <target> | <value> |
	Then the Error has values
	| Message                                   |
	| the value was 'Marry' which was not 'Bob' |
Examples: 
	| description | target | value | table                                                                                                 |
	| simple      | target | Bob   | <tr><th>Key</th><th>Col1</th></tr><tr><td>target</td><td>Marry</td></tr>                              |
	| text input  | target | Bob   | <tr><td>Key</td><td>Col1</td></tr><tr><td>target</td><td><input type="text" value="Marry"/></td></tr> |
