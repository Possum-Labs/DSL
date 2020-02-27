﻿Feature: 2 Templates
	
Background:
	Given the Dealer
	| var     |
	| DEALER1 |
	And the Store
	| var    | Dealer  |
	| STORE1 | DEALER1 |
	And the User
	| var  | Store  | Functions   |
	| USR1 | STORE1 | [['Sales']] |
	And the Inventory Location
	| var  | Dealer  |
	| LOC1 | DEALER1 |
	And Inventory Location 'LOC1' is associated to Store 'STORE1'
	And Logged in as User 'USR1'

Scenario: Sell a Spatula
	Given the Item of type 'Spatula' 
	| var   | Location |
	| ITEM1 | LOC1     |
	And the Item 'Item1' has been damaged
	### do the selling