Feature: Utilizing validators

Scenario: Defaults
	Given the Test Object
		| var |
		| TO  |
	Then 'TO.AString' has the value 'null'
	Then 'TO.AInt' has the value '0'
	Then 'TO.ALong' has the value '0'
	Then 'TO.AFloat' has the value '0'
	Then 'TO.ADecimal' has the value '0'
	Then 'TO.ABool' has the value 'False'


Scenario: IsNull
	Given the Test Object
		| var |
		| TO  |
	Then 'TO.AString' has the value 'null'

Scenario: IsTest
	Given the Test Object
		| var | AInt |
		| TO  | 42   |
	Then 'TO.AInt' has the value '>41'
	And 'TO.AInt' has the value '<43'
	And 'TO.AInt' has the value '<=42'
	And 'TO.AInt' has the value '>=42'

Scenario: IsRegex
	Given the Test Object
		| var | AString |
		| TO  | Bob42   |
	Then 'TO.AString' has the value '/Bob[0-9]+/'

Scenario: IsLitteral
	Given the Test Object
		| var | AString |
		| TO  | TO      |
	Then 'TO.AString' has the value ''TO''
	Then 'TO.AString' has the value '"TO"'

Scenario: IsSubstituted
	Given the Test Object
		| var    | AString |
		| Helper | Bob     |
		| TO     | Bob42      |
	Then 'TO.AString' has the value '`{Helper.AString}42`'

Scenario: IsPercentage
	Given the Test Object
		| var | ADecimal |
		| TO  | .42   |
	Then 'TO.ADecimal' has the value '42%'


Scenario: IsMoney positive 
	Given the Test Object
		| var | ADecimal |
		| TO  | 42.01    |
	Then 'TO.ADecimal' has the value '$42.01'
	And 'TO.ADecimal' has the value '€ 42.01'

Scenario: IsMoney negative 
	Given the Test Object
		| var | ADecimal |
		| TO  | -42.01   |
	Then 'TO.ADecimal' has the value '$(42.01)'
	And 'TO.ADecimal' has the value '-$42.01'
	And 'TO.ADecimal' has the value '$-42.01'
	And 'TO.ADecimal' has the value '€(42.01)'

Scenario: IsNumber

Scenario: IsJson