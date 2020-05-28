Feature: Auto-vend uses smallest open, auto-vend range of given prefix type
	There was a bug where auto-vend used a manual-vend range because it was smallest open.

Scenario Outline: Prefix is auto-vended from smallest open auto-vend range instead of smaller open manual-vend range
	Given I have an account information such that member type is CM, XCM, HCM, NM
	And I have smallest open <prefix_type> range for auto vending with capacity <capacity>
	When I navigate to UIIS UI
	And I log in
	And I open manual-vend <prefix_type> range with capacity <capacity> that is smaller than the auto-vend range
	And I auto-vend a <prefix_type> prefix of capacity <capacity>
	Then I see vended prefix is from the auto-vend range

	@debug
	Examples: 
		| capacity | prefix_type |
		| 100      | UPC         |
		| 10       | EAN         |

	Examples: 
		| capacity | prefix_type |
		| 10       | UPC         |
		| 100      | UPC         |
		| 1000     | UPC         |
		| 10000    | UPC         |
		| 100000   | UPC         |
		| 10       | EAN         |
		| 100      | EAN         |
		| 1000     | EAN         |
		| 10000    | EAN         |
		| 100000   | EAN         |
