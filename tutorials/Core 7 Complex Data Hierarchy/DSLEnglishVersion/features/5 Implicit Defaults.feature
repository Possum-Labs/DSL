Feature: 5 Implicit Defaults

Background:
	Given the User
	| var  | Functions |
	| USR1 | ['Sales'] |
	And Inventory Location '' is associated to Store ''
	And Logged in as User 'USR1'

Scenario: Sell a Spatula
	Given the Item of type 'Spatula' that is 'damaged'
	| var   | 
	| ITEM1 | 
	### do the selling

	Then 'ITEM1.InventoryLocation.Defaulted' has the value 'True'
	And 'ITEM1.InventoryLocation.Dealer.Defaulted' has the value 'True'
	And 'USR1.Store.Defaulted' has the value 'True'
	And 'USR1.Store.Dealer.Defaulted' has the value 'True'
