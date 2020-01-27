 @injected-html
Feature: IFrames


Scenario Outline: Under 
	Given injecting browser content
	| iframe |
	| <html> |
	When under '<under>' entering '<value>' into element '<target>'
	Then under '<under>' the element '<target>' has the value '<value>'

	Examples: 
	| description | under        | target       | value | html                                                                          |
	| 01 simple   | under-target | input-target | Bob   | <div>under-target<label>input-target<input type="text"></input></label></div> |


Scenario Outline: table row
	Given injecting browser content
	| iframe |
	| <html> |
	When for row '<row>' entering '<value>' into element '<target>'
	Then for row '<row>' the element '<target>' has the value '<value>'

	Examples: 
	| description | row  | target | value | html                                                                                          |
	| 01 simple   | row1 | target | Bob   | <table><tr><td>row1<td><td><label>target<input type="text"></input></label></td></tr></table> |

Scenario Outline: entering text inputs
	Given injecting browser content
	| iframe |
	| <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description | target | value | html                                                                    |
	| input for   | target | Bob   | <label for="linky">target</label><input id="linky" type="text"></input> |

Scenario Outline: entering number inputs
	Given injecting browser content
	| iframe |
	| <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description | target | value | html                                                                      |
	| for         | target | 42    | <label for="linky">target</label><input id="linky" type="number"></input> |


Scenario Outline: entering dropdown inputs
	Given injecting browser content
	| iframe |
	| <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description | target | value | html                                                                                                                           |
	| n for       | target | Bob   | <label for="linky">target</label><select id="linky"><option value="bad">Bad</option><option value="test">Bob</option></select> |

Scenario Outline: entering radio
	Given injecting browser content
	| iframe |
	| <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description  | target | value | html                                                                                       |
	| value        | target | Bob   | <input type="radio" id="i1" name="target" value="Bob"></input><label for="i1">noop</label> |

Scenario Outline: clicking 
	Given injecting browser content
	| iframe |
	| <html> |
	When clicking the element '<target>'
Examples: 
	| description | target | html                                                    |
	| 0   a text  | target | <a href = "https://www.w3schools.com/html/" >target</a> |