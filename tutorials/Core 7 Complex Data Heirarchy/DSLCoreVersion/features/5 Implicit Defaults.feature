Feature: 5 Implicit Defaults

Background:
	Given the User
	| var   | Functions   |
	| USR1  | [['Sales']] |
	And Inventory Location '' is associated to Store ''
	And Logged in as User 'USR1'

Scenario: Sell a Spatula
	Given the Item of type 'Spatula' that is 'damaged'
	| var   | 
	| ITEM1 | 
	### do the selling
