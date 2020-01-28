Feature: Settable

Scenario Outline: setting text inputs
	Given injecting browser content
	| Html   |
	| <html> |
	When setting '<value>' for element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description              | target | value | html                                                                                                    |
	| input for                | target | Bob   | <label for="linky">target</label><input id="linky" type="text"></input>                                 |
	| input nested             | target | Bob   | <label>target<input type="text"></input></label>                                                        |
	| input aria-label         | target | Bob   | <input type="text" aria-label="target"></input>                                                         |
	| input aria-labelledby    | t1 t2  | Bob   | <input type="text" aria-labelledby= "l1 l2"></input><label id="l1">t1</label> <label id="l2">t2</label> |
	| textarea for             | target | Bob   | <label for="linky">target</label><textarea id="linky"></textarea>                                       |
	| textarea nested          | target | Bob   | <label>target<textarea></textarea></label>                                                              |
	| textarea aria-label      | target | Bob   | <textarea aria-label="target"></textarea>                                                               |
	| textarea aria-labelledby | t1 t2  | Bob   | <textarea aria-labelledby= "l1 l2"></textarea><label id="l1">t1</label><label id="l2">t2</label>        |
	| input nested deeper      | target | Bob   | <label><span><strong>target</strong></span><span><input type="text"></span></label>                     |


Scenario Outline: setting number inputs
	Given injecting browser content
	| Html   |
	| <html> |
	When setting '<value>' for element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description     | target | value | html                                                                                                      |
	| for             | target | 42    | <label for="linky">target</label><input id="linky" type="number"></input>                                 |
	| nested          | target | 42    | <label>target<input type="number"></input></label >                                                       |
	| aria-label      | target | 42    | <input type="number" aria-label="target"></input>                                                         |
	| aria-labelledby | t1 t2  | 42    | <input type="number" aria-labelledby= "l1 l2"></input><label id="l1">t1</label> <label id="l2">t2</label> |

Scenario Outline: setting dropdown inputs
	Given injecting browser content
	| Html   |
	| <html> |
	When setting '<value>' for element '<target>'
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

Scenario Outline: setting radio
	Given injecting browser content
	| Html   |
	| <html> |
	When setting '<value>' for element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description  | target | value | html                                                                                       |
	| value        | target | Bob   | <input type="radio" id="i1" name="target" value="Bob"></input><label for="i1">noop</label> |
	| label for    | target | Bob   | <input type="radio" id="i1" name="target" value="noop"></input><label for="i1">Bob</label> |
	| label nested | target | Bob   | <label>Bob<input type="radio" id="i1" name="target" value="noop"></input></label>          |


Scenario Outline: setting checkboxes
	Given injecting browser content
	| Html   |
	| <html> |
	When setting '<value>' for element '<target>'
	Then the element '<target>' has the value '<value>'
Examples: 
	| description         | target | value     | html                                                                                          |
	| value               | target | checked   | <input type="checkbox" id="i1" name="target" value="Bob"></input><label for="i1">noop</label> |
	| label for           | target | checked   | <input type="checkbox" id="i1" name="target" value="noop"></input><label for="i1">Bob</label> |
	| label nested        | target | checked   | <label>Bob<input type="checkbox" name="target" value="noop"></input></label>                  |
	| no value            | target | checked   | <label>target<input type="checkbox"></input></label>                                          |
	| checked             | target | checked   | <label>target<input type="checkbox" checked></input></label>                                  |
	| unchecking          | target | unchecked | <label>target<input type="checkbox"></input></label>                                          |
	| unchecking  checked | target | unchecked | <label>target<input type="checkbox" checked></input></label>                                  |


Scenario Outline: error messages
	Given injecting browser content
	| Html   |
	| <html> |
	Given an error is expected
	When setting '<value>' for element '<target>'
	Then the Error has values
    | Message |
    | <error> |
Examples: 
	| description | target | value   | html                                                                                     | error                   |
	| value       | target | checked | <input type="checkbox" id="i1" name="t" value="Bob"></input><label for="i1">noop</label> | /element was not found/ |
