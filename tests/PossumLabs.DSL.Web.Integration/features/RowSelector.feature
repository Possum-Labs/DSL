 @injected-html
Feature: Row Selector

Scenario Outline: table row
	Given injecting browser content
	| TableRow |
	| <html>   |
	When for row '<row>' entering '<value>' into element '<target>'
	Then for row '<row>' the element '<target>' has the value '<value>'

	Examples: 
	| description | row  | target | value | html                                                                                          |
	| 01 simple   | row1 | target | Bob   | <td>row1<td><td><label>target<input type="text"></input></label></td>                         |
	| 02 label    | row1 | target | Bob   | <td><label>row1</label><td><td><label>target<input type="text"></input></label></td>          |
	| 03 b        | row1 | target | Bob   | <td><b>row1</b><td><td><label>target<input type="text"></input></label></td>                  |
	| 04 h1       | row1 | target | Bob   | <td><h1>row1</h1><td><td><label>target<input type="text"></input></label></td>                |
	| 05 h2       | row1 | target | Bob   | <td><h2>row1</h2><td><td><label>target<input type="text"></input></label></td>                |
	| 06 h3       | row1 | target | Bob   | <td><h3>row1</h3><td><td><label>target<input type="text"></input></label></td>                |
	| 07 h4       | row1 | target | Bob   | <td><h4>row1</h4><td><td><label>target<input type="text"></input></label></td>                |
	| 08 h5       | row1 | target | Bob   | <td><h5>row1</h5><td><td><label>target<input type="text"></input></label></td>                |
	| 09 h6       | row1 | target | Bob   | <td><h6>row1</h6><td><td><label>target<input type="text"></input></label></td>                |
	| 10 input    | row1 | target | Bob   | <td><input value="row1"></input><td><td><label>target<input type="text"></input></label></td> |
	| 11 span     | row1 | target | Bob   | <td><span>row1</span><td><td><label>target<input type="text"></input></label></td>            |

Scenario Outline: div row
	Given injecting browser content
	| Html |
	| <html>   |
	When for row '<row>' entering '<value>' into element '<target>'
	Then for row '<row>' the element '<target>' has the value '<value>'

	Examples: 
	| description | row  | target | value | html                                                                                               |
	| 01 simple   | row1 | target | Bob   | <div role="row">row1<label>target<input type="text"></input></label></div>                         |
	| 02 label    | row1 | target | Bob   | <div role="row"><label>row1</label><label>target<input type="text"></input></label></div>          |
	| 03 b        | row1 | target | Bob   | <div role="row"><b>row1</b><label>target<input type="text"></input></label></div>                  |
	| 04 h1       | row1 | target | Bob   | <div role="row"><h1>row1</h1><label>target<input type="text"></input></label></div>                |
	| 05 h2       | row1 | target | Bob   | <div role="row"><h2>row1</h2><label>target<input type="text"></input></label></div>                |
	| 06 h3       | row1 | target | Bob   | <div role="row"><h3>row1</h3><label>target<input type="text"></input></label></div>                |
	| 07 h4       | row1 | target | Bob   | <div role="row"><h4>row1</h4><label>target<input type="text"></input></label></div>                |
	| 08 h5       | row1 | target | Bob   | <div role="row"><h5>row1</h5><label>target<input type="text"></input></label></div>                |
	| 09 h6       | row1 | target | Bob   | <div role="row"><h6>row1</h6><label>target<input type="text"></input></label></div>                |
	| 10 input    | row1 | target | Bob   | <div role="row"><input value="row1"></input><label>target<input type="text"></input></label></div> |
	| 11 span     | row1 | target | Bob   | <div role="row"><span>row1</span><label>target<input type="text"></input></label></div>            |



