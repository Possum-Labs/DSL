@SingleBrowser @injected-html
Feature: UnderSelector

Scenario Outline: Div 
	Given injecting browser content
	| Html   |
	| <html> |
	When under '<under>' entering '<value>' into element '<target>'
	Then under '<under>' the element '<target>' has the value '<value>'

	Examples: 
	| description | under        | target       | value | html                                                                                                  |
	| 01 simple   | under-target | input-target | Bob   | <div>under-target<label>input-target<input type="text"></input></label></div>                         |
	| 02 label    | under-target | input-target | Bob   | <div><label>under-target</label><label>input-target<input type="text"></input></label></div>          |
	| 03 b        | under-target | input-target | Bob   | <div><b>under-target</b><label>input-target<input type="text"></input></label></div>                  |
	| 04 h1       | under-target | input-target | Bob   | <div><h1>under-target</h1><label>input-target<input type="text"></input></label></div>                |
	| 05 h2       | under-target | input-target | Bob   | <div><h2>under-target</h2><label>input-target<input type="text"></input></label></div>                |
	| 06 h3       | under-target | input-target | Bob   | <div><h3>under-target</h3><label>input-target<input type="text"></input></label></div>                |
	| 07 h4       | under-target | input-target | Bob   | <div><h4>under-target</h4><label>input-target<input type="text"></input></label></div>                |
	| 08 h5       | under-target | input-target | Bob   | <div><h5>under-target</h5><label>input-target<input type="text"></input></label></div>                |
	| 09 h6       | under-target | input-target | Bob   | <div><h6>under-target</h6><label>input-target<input type="text"></input></label></div>                |
	| 10 input    | under-target | input-target | Bob   | <div><input value="under-target"></input><label>input-target<input type="text"></input></label></div> |
	| 11 span     | under-target | input-target | Bob   | <div><span>under-target</span><label>input-target<input type="text"></input></label></div>            |


Scenario Outline: Div with row role 
	Given injecting browser content
	| Html   |
	| <html> |
	When under '<under>' entering '<value>' into element '<target>'
	Then under '<under>' the element '<target>' has the value '<value>'

	Examples: 
	| description | under        | target       | value | html                                                                                                                        |
	| 01 simple   | under-target | input-target | Bob   | <div role="row"><div>under-target<label>input-target<input type="text"></input></label></div></div>                         |
	| 02 label    | under-target | input-target | Bob   | <div role="row"><div><label>under-target</label><label>input-target<input type="text"></input></label></div></div>          |
	| 03 b        | under-target | input-target | Bob   | <div role="row"><div><b>under-target</b><label>input-target<input type="text"></input></label></div></div>                  |
	| 04 h1       | under-target | input-target | Bob   | <div role="row"><div><h1>under-target</h1><label>input-target<input type="text"></input></label></div></div>                |
	| 05 h2       | under-target | input-target | Bob   | <div role="row"><div><h2>under-target</h2><label>input-target<input type="text"></input></label></div></div>                |
	| 06 h3       | under-target | input-target | Bob   | <div role="row"><div><h3>under-target</h3><label>input-target<input type="text"></input></label></div></div>                |
	| 07 h4       | under-target | input-target | Bob   | <div role="row"><div><h4>under-target</h4><label>input-target<input type="text"></input></label></div></div>                |
	| 08 h5       | under-target | input-target | Bob   | <div role="row"><div><h5>under-target</h5><label>input-target<input type="text"></input></label></div></div>                |
	| 09 h6       | under-target | input-target | Bob   | <div role="row"><div><h6>under-target</h6><label>input-target<input type="text"></input></label></div></div>                |
	| 10 input    | under-target | input-target | Bob   | <div role="row"><div><input value="under-target"></input><label>input-target<input type="text"></input></label></div></div> |
	| 11 span     | under-target | input-target | Bob   | <div role="row"><div><span>under-target</span><label>input-target<input type="text"></input></label></div></div>            |