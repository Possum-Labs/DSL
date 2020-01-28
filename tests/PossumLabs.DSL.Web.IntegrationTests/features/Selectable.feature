Feature: Selectable selector

Scenario Outline: selecting inputs
	Given injecting browser content
	| Html   |
	| <html> |
	When selecting '<value>' for element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description        | target | value | html                                                                                                                                                            |
	| n for              | target | Bob   | <label for="linky">target</label><select id="linky"><option value="bad">Bad</option><option value="test">Bob</option></select>                                  |
	| n nested           | target | Bob   | <label>target<select><option value="bad">Bad</option><option value="test">Bob</option></select></label >                                                        |
	| n aria-label       | target | Bob   | <select aria-label="target"><option value="bad">Bad</option><option value="test">Bob</option></select>                                                          |
	| n name             | target | Bob   | <select name="target"><option value="bad">Bad</option><option value="test">Bob</option></select>                                                                |
	| n aria-labelledby  | t1 t2  | Bob   | <select aria-labelledby= "l1 l2"><option value="bad">Bad</option><option value="test">Bob</option></select><label id="l1">t1</label><label id="l2">t2</label>   |
	| v for              | target | Bob   | <label for="linky">target</label><select id="linky"><option value="bad">Bad</option><option value="Bob">test</option></select>                                  |
	| v nested           | target | Bob   | <label>target<select><option value="bad">Bad</option><option value="Bob">test</option></select></label >                                                        |
	| v aria-label       | target | Bob   | <select aria-label="target"><option value="bad">Bad</option><option value="Bob">test</option></select>                                                          |
	| v name             | target | Bob   | <select name="target"><option value="bad">Bad</option><option value="Bob">test</option></select>                                                                |
	| v aria-labelledby  | t1 t2  | Bob   | <select aria-labelledby= "l1 l2"><option value="bad">Bad</option><option value="Bob">test</option></select><label id="l1">t1</label><label id="l2">t2</label>   |
	| dl for             | target | Bob   | <label for="linky">target</label><input id="linky" list="o"><datalist id="o"><option value="bad"><option value="Bob"></datalist></select>                       |
	| dl nested          | target | Bob   | <label>target<input list="o"><datalist id="o"><option value="bad"><option value="Bob"></datalist></label >                                                      |
	| dl aria-label      | target | Bob   | <input aria-label="target" list="o"><datalist id="o"><option value="bad"><option value="Bob"></datalist>                                                        |
	| dl aria-labelledby | t1 t2  | Bob   | <input aria-labelledby= "l1 l2" list="o"><datalist id="o"><option value="bad"><option value="Bob"></datalist><label id="l1">t1</label><label id="l2">t2</label> |
	| default name       | target | Bob   | <select name="target"><option value="" groupname displayorder="-1"></option><option value="bad">Bad</option><option value="Bob">test</option></select>          |

Scenario Outline: error messages selecting
	Given injecting browser content
	| Html   |
	| <html> |
	Given an error is expected
	When selecting '<value>' for element '<target>'
	Then the Error has values
    | Message |
    | <error> |
Examples: 
	| description | target | value   | html                         | error                   |
	| value       | target | bob     | <label>target<input></label> | /element was not found/ |