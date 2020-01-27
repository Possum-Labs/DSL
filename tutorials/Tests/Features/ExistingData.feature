Feature: ExistingData

Scenario: check existing user data
	Then 'User.Password' has the value 'demouser'
	Then 'Admin.Password' has the value 'demoadmin'
	Then 'Supplier.Password' has the value 'demosupplier'

