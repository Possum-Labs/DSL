Feature: Web Element Properties
	Rigging for Selenium 4 testing.


Scenario: Displayed true
	Given injecting browser content
	| Html																	  |
	| <label for="linky">target</label><input id="linky" type="text"></input> |
	When setting 'bob' for element 'target'
	Given the Settable Element 'E1' found by 'target'
	Then 'E1.Displayed' has the value 'True'

Scenario: Displayed display:none
	Given injecting browser content
	| Html														   |
	| <input id="target" type="text" style="display:none"></input> |
	Given the Settable Element 'E1' found by '#target'
	Then 'E1.Displayed' has the value 'False'

Scenario: Displayed visibility:hidden
	Given injecting browser content
	| Html																|
	| <input id="target" type="text" style="visibility:hidden"></input> |
	Given the Settable Element 'E1' found by '#target'
	Then 'E1.Displayed' has the value 'False'

Scenario: Tag Name
	Given injecting browser content
	| Html														   |
	| <input id="target" type="text" style="display:none"></input> |
	Given the Settable Element 'E1' found by '#target'
	Then 'E1.TagName' has the value 'input'

Scenario: Unknown Attribute (list)
	Given injecting browser content
	| Html														   |
	| <input id="target" type="text"></input> |
	Given the Settable Element 'E1' found by '#target'
	Then Element 'E1' Attribute 'list' has the value 'null'