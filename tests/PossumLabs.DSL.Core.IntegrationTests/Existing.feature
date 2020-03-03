Feature: Existing

Scenario: Defaults with default template
	Then 'Existing.ExistingName' has the value ''Existing''
	Then 'Existing.TemplateName' has the value 'default'

Scenario: Defaults with specific template
	Then 'OtherExisting.ExistingName' has the value 'Other'
	Then 'OtherExisting.TemplateName' has the value 'A'
