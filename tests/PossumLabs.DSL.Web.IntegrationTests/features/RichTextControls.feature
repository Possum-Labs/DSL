Feature: Rich Text Controls
all id's for the text area should be id="myeditor"


Scenario Outline: entering text inputs CKEditor 4
	Given injecting browser content
	| CKEditor4 |
	|    <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '/<result>/'
Examples: 
	| description              | target | value | result       | html                                                                                                           |
	| textarea for             | target | Bob   | ^<p>Bob<\/p> | <label for="myeditor">target</label><textarea id="myeditor"></textarea>                                        |
	| textarea following       | target | Bob   | ^<p>Bob<\/p> | <label>target</label><textarea id="myeditor"></textarea>                                                       |
	| textarea nested          | target | Bob   | ^<p>Bob<\/p> | <label>target<textarea id="myeditor"></textarea></label>                                                       |
	| textarea aria-label      | target | Bob   | ^<p>Bob<\/p> | <textarea id="myeditor" aria-label="target"></textarea>                                                        |
	| textarea aria-labelledby | t1 t2  | Bob   | ^<p>Bob<\/p> | <textarea id="myeditor" aria-labelledby= "l1 l2"></textarea><label id="l1">t1</label><label id="l2">t2</label> |
	
	
Scenario Outline: entering text inputs CKEditor 5
	Given injecting browser content
	| CKEditor5 |
	|    <html> |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '<result>'
Examples: 
	| description              | target | value | result | html                                                                                                           |
	| textarea for             | target | Bob   | Bob    | <label for="myeditor">target</label><textarea id="myeditor"></textarea>                                        |
	| textarea following       | target | Bob   | Bob    | <label>target</label><textarea id="myeditor"></textarea>                                                       |
	| textarea nested          | target | Bob   | Bob    | <label>target<textarea id="myeditor"></textarea></label>                                                       |
	| textarea aria-label      | target | Bob   | Bob    | <textarea id="myeditor" aria-label="target"></textarea>                                                        |
	| textarea aria-labelledby | t1 t2  | Bob   | Bob    | <textarea id="myeditor" aria-labelledby= "l1 l2"></textarea><label id="l1">t1</label><label id="l2">t2</label> |
	
Scenario Outline: entering text inputs Tiny MCE 4.5.5
	Given injecting browser content
	| TinyMCE4 |
	|  <html>  |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '/<result>/'
Examples: 
	| description              | target | value | result       | html                                                                                                           |
	| textarea for             | target | Bob   | ^<p>Bob<\/p> | <label for="myeditor">target</label><textarea id="myeditor"></textarea>                                        |
	| textarea following       | target | Bob   | ^<p>Bob<\/p> | <label>target</label><textarea id="myeditor"></textarea>                                                       |
	| textarea nested          | target | Bob   | ^<p>Bob<\/p> | <label>target<textarea id="myeditor"></textarea></label>                                                       |
	| textarea aria-label      | target | Bob   | ^<p>Bob<\/p> | <textarea id="myeditor" aria-label="target"></textarea>                                                        |
	| textarea aria-labelledby | t1 t2  | Bob   | ^<p>Bob<\/p> | <textarea id="myeditor" aria-labelledby= "l1 l2"></textarea><label id="l1">t1</label><label id="l2">t2</label> |
	
Scenario Outline: entering text inputs Tiny MCE 5
	Given injecting browser content
	| TinyMCE5 |
	|  <html>  |
	When entering '<value>' into element '<target>'
	Then the element '<target>' has the value '/<result>/'
Examples: 
	| description              | target | value | result       | html                                                                                                           |
	| textarea for             | target | Bob   | ^<p>Bob<\/p> | <label for="myeditor">target</label><textarea id="myeditor"></textarea>                                        |
	| textarea following       | target | Bob   | ^<p>Bob<\/p> | <label>target</label><textarea id="myeditor"></textarea>                                                       |
	| textarea nested          | target | Bob   | ^<p>Bob<\/p> | <label>target<textarea id="myeditor"></textarea></label>                                                       |
	| textarea aria-label      | target | Bob   | ^<p>Bob<\/p> | <textarea id="myeditor" aria-label="target"></textarea>                                                        |
	| textarea aria-labelledby | t1 t2  | Bob   | ^<p>Bob<\/p> | <textarea id="myeditor" aria-labelledby= "l1 l2"></textarea><label id="l1">t1</label><label id="l2">t2</label> |
