Feature: Range management regression scenarios


Scenario Outline: Open range for automatic vending
    Given I have a <prefix_type> RANGE for capacity <capacity> which is not open
	When I navigate to UIIS UI
	And I log in
	And I select Open a Prefix Range menu
	And I click a RANGE button for <prefix_type>
	And I select <capacity> capacity radio button
	And I input the specific range
	And I click on Enable Automatic Vending checkbox
	And I input the reason for opening the range
	And I click on Next button in Open Prefix Range page
	And click on the OPEN RANGE button
	Then I see this range removed from companyPrefixAvailableRange table
	And the range predicate get added to CompanyPrefixRange table
	And I see top of the page show open confirmation message Prefix Range Opened
	And I see in CompanyPrefixRange table AutoAssign = 1

	@debug @light
	Examples:
		| capacity | prefix_type |
		| 10       | UPC         |

	@debug
	Examples:
		| capacity | prefix_type |
		| 10       | UPC         |
		| 100      | EAN         |
	
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


Scenario Outline: Open range for specific vending
    Given I have a <prefix_type> RANGE for specific vending for capacity <capacity> which is not open
	When I navigate to UIIS UI
	And I log in
	And I select Open a Prefix Range menu
	And I click a RANGE button for <prefix_type>
	And I select <capacity> capacity radio button
	And I input the specific range
	And I input the reason for opening the range
	And I click on Next button in Open Prefix Range page
	And click on the OPEN RANGE button
	Then the range predicate get added to CompanyPrefixRange table
	And I see this range unavailable from companyPrefixAvailableRange table
	And I see top of the page show open confirmation message Prefix Range Opened
	And I see in CompanyPrefixRange table AutoAssign = 0

	@light
	Examples:
		| capacity | prefix_type |
		| 100      | EAN         |

	@debug
	Examples:
		| capacity | prefix_type |
		| 10000    | UPC         |
		| 100      | EAN         |

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


@debug @light
Scenario: Close a RANGE for auto vending
	Given I have a open range for auto vending
	When I navigate to UIIS UI
	And I log in
	And I select Close a Prefix Range menu
	And I input the range which is open
	And I input the reason for closing the range
	And I click on the NEXT button on Close a Prefix Range page
	And I click on CLOSE RANGE button on Review page
	Then the range predicate gets removed from CompanyPrefixRange table
	#And I see this range in CompanyPrefixAvailableRange table
	# -- this does not happen unconditionally
	# -- it only happens to ranges that are earmarked for auto-vending
	And I see top of the page show close confirmation message Prefix range has been closed
	

@debug @light
Scenario: Close a RANGE for manual vending
	Given I have a open range for manual vending
	When I navigate to UIIS UI
	And I log in
	And I select Close a Prefix Range menu
	And I input the range which is open
	And I input the reason for closing the range
	And I click on the NEXT button on Close a Prefix Range page
	And I click on CLOSE RANGE button on Review page
	Then the range predicate gets removed from CompanyPrefixRange table
	And I see top of the page show close confirmation message Prefix range has been closed

