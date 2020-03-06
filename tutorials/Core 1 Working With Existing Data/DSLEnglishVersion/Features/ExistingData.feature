Feature: Existing Data
	this feature assumes that you have set up the environment variable. See SetEnvironmentVariable.cmd

Scenario: Admin user email from Existing.json
	Then 'Admin.Email' has the value 'admin@possumlabs.com'

Scenario: Admin user Password overwritten by Environment variable
	Then 'Admin.Password' has the value 'Sup3rS3cret'

Scenario: Guest user email from Existing.json
	Then 'Guest.Email' has the value 'guest@possumlabs.com'

Scenario: Guest user password from Existing.json
	Then 'Guest.Password' has the value 'P@ssw0rd'