Feature: Prefix holding and releasing regression scenarios


@debug @light
Scenario: On Hold Prefixes page UI test
	When I navigate to UIIS UI
	And I log in
	And I click on link On Hold Prefix
	Then I get multiple columns such as Prefix On Hold, Capacity, Type, Account, Date, Who, Reason, Actions 
	And All the columns are sortable
	And Filter columns fetches the desired data based upon filter conditions
	And items page fetches the count based upon the selection


Scenario Outline: Hold prefix
	Given I have an account information such that member type is CM, XCM, HCM or NM
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I click on HOLD A PREFIX button
	And I input account number
	And I click on validate button
	And I click on the <prefix_type> PREFIX button
	And I select <capacity> capacity radio button
	And I select <prefix_type> prefix from CompanyPrefix table
	And I input selected prefix 
	And I verify Prefix Available
	And I enter reason for holding the prefix
	And I click on the NEXT button
	And I click on Hold PREFIX button on Review page
	And I check this Identifier available in CompanyPrefixHold table

	@debug
	Examples:
		| capacity | prefix_type |
		| 10       | UPC         |
		| 100      | UPC         |
		| 100      | EAN         |
		| 1000     | EAN         |
		
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


Scenario Outline: Release an held prefix
	Given I have a held <prefix_type> prefix
	When I navigate to UIIS UI
	And I log in
	And I click on link On Hold Prefix
	And I enter the prefix in the filter field
	And I click on Release button in the first row
	And I get the review page showing Account Number, Identifier Type, Capacity, Prefix, Reason for Hold
	And I click on Yes button
	Then I check this prefix is not available in CompanyPrefixHold table
	And I check this prefix is not in CompanyPrefix table
	
	@debug
	Examples:
		| prefix_type |
		| UPC         |
		| EAN         |


@debug
Scenario: Release and vend held UPC prefix
	Given I have a held UPC prefix
	When I navigate to UIIS UI
	And I log in
	And I click on link On Hold Prefix
	And I enter the prefix in the filter field
	And I click on Release and Vend button in the first row
	And I get the review page showing Account Number, Identifier Type, Capacity, Prefix, Reason
	And I click on vend prefix button 
	And I wait for sync to be successful
	Then I check that displayed vended prefix and account number is correct
	And I check this prefix is not available in CompanyPrefixHold table
	And I check this prefix in CompanyPrefix table
	And I check that vended identifier matches major key in Name table with leading 0 removed
	And I check that member type is #UPC
	And I check that category is CAP


@debug
Scenario: Release and vend EAN prefix
    Given I have a held EAN prefix
	When I navigate to UIIS UI
	And I log in
	And I click on link On Hold Prefix
	And I enter the prefix in the filter field
	And I click on Release and Vend button in the first row
	And I get the review page showing Account Number, Identifier Type, Capacity, Prefix, Reason
	And I click on vend prefix button 
	And I wait for sync to be successful
	Then I check that displayed vended prefix and account number is correct
	And  I check this prefix is not available in CompanyPrefixHold table
	And I check this prefix in CompanyPrefix table
	And I check that vended identifier matches last name in Name table
	And I check that member type is #UPC
	And I check that category is SPC
