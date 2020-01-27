@SingleBrowser
Feature: phpTravels
https://phptravels.com/demo/

Scenario: Find flights -- clicking and select
	Given navigated to 'https://www.phptravels.net/'
	When entering 'Denv' into element 'hotel_s2_text'
	And clicking the element 'Denver, United States'
	And selecting the element 'Check in' 
	And clicking the element 'Today.Day'
	And clicking the element 'Tomorrow.Day'
	And clicking the element 'Search'
	Then the page contains the element 'No Results Found'


Scenario: hotel details -- naviagting items in complex scenarios (rows & under)
	Given navigated to 'https://www.phptravels.net/hotels/listing/'
	When under 'Hyatt Regency Perth' click the element 'Details' 
	And for the row 'Hyatt Regency Perth' click the element 'Details'
	Then the row 'EXECUTIVE TWO-BEDROOMS APARTMENT' contains the element '$300' 


