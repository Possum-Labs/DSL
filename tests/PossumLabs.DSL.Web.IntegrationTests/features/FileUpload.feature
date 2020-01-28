Feature: File Upload tests

Scenario Outline: simple file control
	Given injecting browser content
	| Html   |
	| <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '<newValue>'
Examples: 
	| description | target | value            | newValue             | html                                                                    |
	| input for   | target | c:\temp\temp.txt | C:\fakepath\temp.txt | <label for="linky">target</label><input id="linky" type="file"></input> |
