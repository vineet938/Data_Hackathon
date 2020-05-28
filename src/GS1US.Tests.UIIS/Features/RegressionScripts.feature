Feature: UIIS regression scripts
	Test scenarios in UIIS regression scripts

Scenario Outline: UPC prefix auto vending 
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I observe validation passes with green check mark
	And I click on the UPC PREFIX button
	And I click on the STANDARD UPC PREFIX button
	And I select <capacity> capacity radio button
	And I click on the NEXT AVAILABLE button
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches major key in Name table with leading 0 removed
	And I check that member type is #UPC
	And I check that category is CAP
	And I see a new entity GLN displayed
	And If <case> = Existing, I check that displayed GLN does not match ENTITY_GLN in Demog_All_W table
	And If <case> = New, I check that displayed GLN does match ENTITY_GLN in Demog_All_W table
	And I check that account information is correct
	And I check that identifier type is UPC Prefix
	And I check that capacity is <capacity>
	And I check that UPC Range is Standard UPC Prefix

	@debug @light
	Examples:
		| capacity | member_type | case     |
		| 10       | CM          | Existing |
		| 100      | NM          | New      |

	Examples:
		| capacity | member_type | case     |
		| 10       | CM          | Existing |
		| 100      | CM          | Existing |
		| 1000     | CM          | Existing |
		| 10000    | CM          | Existing |
		| 100000   | CM          | Existing |
		| 10       | NM          | New      |
		| 100      | NM          | New      |
		| 1000     | NM          | New      |
		| 10000    | NM          | New      |
		| 100000   | NM          | New      |
		| 10       | XCM         | New      |
		| 100      | XCM         | New      |
		| 1000     | XCM         | New      |
		| 10000    | XCM         | New      |
		| 100000   | XCM         | New      |
		| 10       | HCM         | New      |
		| 100      | HCM         | New      |
		| 1000     | HCM         | New      |
		| 10000    | HCM         | New      |
		| 100000   | HCM         | New      |
		
Scenario Outline: EAN prefix auto vending
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I observe validation passes with green check mark
	And I click on EAN PREFIX button
	And I select <capacity> capacity radio button
	And I click on the NEXT AVAILABLE button
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches last name in Name table
	And I check that member type is #UPC
	And I check that category is SPC
	And I see a new entity GLN displayed
	And If <case> = Existing, I check that displayed GLN does not match ENTITY_GLN in Demog_All_W table
	And If <case> = New, I check that displayed GLN does match ENTITY_GLN in Demog_All_W table
	And I check that account information is correct
	And I check that identifier type is EAN Prefix
	And I check that capacity is <capacity>

	@debug @light
	Examples:
		| capacity | member_type |case		|
		| 10       | CM          | Existing |
		| 1000     | NM          | New      |

	Examples:
		| capacity | member_type |case		|
		| 10       | CM          | Existing |
		| 100      | CM          | Existing |
		| 1000     | CM          | Existing |
		| 10000    | CM          | Existing |
		| 100000   | CM          | Existing |
		| 10       | NM          | New      |
		| 100      | NM          | New      |
		| 1000     | NM          | New      |
		| 10000    | NM          | New      |
		| 100000   | NM          | New      |
		| 10       | XCM         | New      |
		| 100      | XCM         | New      |
		| 1000     | XCM         | New      |
		| 10000    | XCM         | New      |
		| 100000   | XCM         | New      |
		| 10       | HCM         | New      |
		| 100      | HCM         | New      |
		| 1000     | HCM         | New      |
		| 10000    | HCM         | New      |
		| 100000   | HCM         | New      |


@debug
Scenario: EDI COMM ID prefix auto vending
	Given I have a CM account with an active prefix
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I observe validation passes with green check mark
	And I click on EDI COMM ID button
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check that vended identifier matches major key in Name table
	And I check that member type is #UCS
	And I check that account information is correct
	And I check that identifier type is EDI Comm ID


Scenario Outline: UPC specific prefix vending
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I observe validation passes with green check mark
	And I click on the UPC PREFIX button
	And I click on the STANDARD UPC PREFIX button
	And I select <capacity> capacity radio button
	And I click on the SPECIFIC PREFIX button
	And I select UPC prefix from CompanyPrefix table 
	And I input selected prefix 
	And I verify Prefix Available
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches major key in Name table with leading 0 removed
	And I check that member type is #UPC
	And I check that category is CAP
	And I see a new entity GLN displayed
	And If <case> = Existing, I check that displayed GLN does not match ENTITY_GLN in Demog_All_W table
	And If <case> = New, I check that displayed GLN does match ENTITY_GLN in Demog_All_W table
	And I check that account information is correct
	And I check that identifier type is UPC Prefix
	And I check that capacity is <capacity>
	And I check that UPC Range is Standard UPC Prefix

	@debug @light
	Examples:
		| capacity | member_type |case		|
		| 100      | CM          | Existing |
		| 1000     | HCM         | New      |

	Examples:
		| capacity | member_type |case		|
		| 10       | CM          | Existing |
		| 100      | CM          | Existing |
		| 1000     | CM          | Existing |
		| 10000    | CM          | Existing |
		| 100000   | CM          | Existing |
		| 10       | NM          | New      |
		| 100      | NM          | New      |
		| 1000     | NM          | New      |
		| 10000    | NM          | New      |
		| 100000   | NM          | New      |
		| 10       | XCM         | New      |
		| 100      | XCM         | New      |
		| 1000     | XCM         | New      |
		| 10000    | XCM         | New      |
		| 100000   | XCM         | New      |
		| 10       | HCM         | New      |
		| 100      | HCM         | New      |
		| 1000     | HCM         | New      |
		| 10000    | HCM         | New      |
		| 100000   | HCM         | New      |


Scenario Outline: EAN specific prefix vending
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I observe validation passes with green check mark
	And I click on the EAN PREFIX button
	And I select <capacity> capacity radio button
	And I click on the SPECIFIC PREFIX button
	And I select EAN prefix from CompanyPrefix table
	And I input selected prefix 
	And I verify Prefix Available
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches last name in Name table
	And I check that member type is #UPC
	And I check that category is SPC
	And I see a new entity GLN displayed
	And If <case> = existing, I check that displayed GLN does not match ENTITY_GLN in Demog_All_W table
	And If <case> = New, I check that displayed GLN does match ENTITY_GLN in Demog_All_W table
	And I check that account information is correct
	And I check that identifier type is EAN Prefix
	And I check that capacity is <capacity>

	@debug @light
	Examples:
		| capacity | member_type |case		|
		| 1000     | CM          | Existing |
		| 1000     | NM          | New      |

	Examples:
		| capacity | member_type |case		|
		| 10       | CM          | Existing |
		| 100      | CM          | Existing |
		| 1000     | CM          | Existing |
		| 10000    | CM          | Existing |
		| 100000   | CM          | Existing |
		| 10       | NM          | New      |
		| 100      | NM          | New      |
		| 1000     | NM          | New      |
		| 10000    | NM          | New      |
		| 100000   | NM          | New      |
		| 10       | XCM         | New      |
		| 100      | XCM         | New      |
		| 1000     | XCM         | New      |
		| 10000    | XCM         | New      |
		| 100000   | XCM         | New      |
		| 10       | HCM         | New      |
		| 100      | HCM         | New      |
		| 1000     | HCM         | New      |
		| 10000    | HCM         | New      |
		| 100000   | HCM         | New      |

		
Scenario Outline: NDC prefix vending
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I click on the UPC PREFIX button
	And I click on the NDC PREFIX button
	And I select labeler code from LabelerCode table
	And I enter the labeler code
	And I click VALIDATE CODE button
	And I check the company name matches with LabelerCode table
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches major key in Name table
	And I check that member type is #UPC
	And I check that category is CAP
	And I see a new entity GLN displayed
	And I check that account information is correct
	And I check that identifier type is UPC Prefix
	And I check that UPC Range is NDC Prefix

	@debug @light
	Examples:
		| member_type |
		| XCM         |

	Examples:
		| member_type |
		| CM          |
		| XCM         |
		| HCM         |
		| NM          |

		
Scenario Outline: NDC Prefix vending with invalid labeler code
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I click on the UPC PREFIX button
	And I click on the NDC PREFIX button
	And I enter invalid labeler code: 1111
	And I click VALIDATE CODE button
	Then I check that labeler code validation error message is displayed

	@debug @light
	Examples:
		| member_type |
		| CM          |

	Examples:
		| member_type |
		| CM          |
		| XCM         |
		| HCM         |
		| NM          |


@debug @light
Scenario: Account Number validation
	Given I have an invalid account number
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I enter the invalid account number
	And I click on validate button
	Then I see account number validation error


@debug @light
Scenario: Short account number validation
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I enter a short account number
	Then I observe that validate button is disable


Scenario Outline: Alliance prefix auto vending
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I click on the UPC PREFIX button
	And I click on the ALLIANCE PREFIX button
	And I select <capacity> capacity radio button
	And I click on the NEXT AVAILABLE button
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches major key in Name table with leading 0 removed
	And I check that member type is #UPC
	And I check that category is MO
	And I see a new entity GLN displayed
	And If <case> = Existing, I check that displayed GLN does not match ENTITY_GLN in Demog_All_W table
	And If <case> = New, I check that entity GLN is not created in Demog_All_W table
	And I check that account information is correct
	And I check that identifier type is UPC Prefix
	And I check that capacity is <capacity>
	And I check that UPC Range is Alliance Prefix

	@debug @light
	Examples:
		| capacity | member_type | case     |
		| 10000    | CM          | Existing |
		| 1000     | NM          | New      |
		| 10000    | AO          | N/A      |

	Examples:
		| capacity | member_type |case		|
		| 10       | CM          | Existing |
		| 100      | CM          | Existing |
		| 1000     | CM          | Existing |
		| 10000    | CM          | Existing |
		| 100000   | CM          | Existing |
		| 10       | NM          | New      |
		| 100      | NM          | New      |
		| 1000     | NM          | New      |
		| 10000    | NM          | New      |
		| 100000   | NM          | New      |
		| 10       | XCM         | New      |
		| 100      | XCM         | New      |
		| 1000     | XCM         | New      |
		| 10000    | XCM         | New      |
		| 100000   | XCM         | New      |
		| 10       | HCM         | New      |
		| 100      | HCM         | New      |
		| 1000     | HCM         | New      |
		| 10000    | HCM         | New      |
		| 100000   | HCM         | New      |
		| 10       | AO          | N/A      |
		| 100      | AO          | N/A      |
		| 1000     | AO          | N/A      |
		| 10000    | AO          | N/A      |
		| 100000   | AO          | N/A      |

Scenario Outline: Alliance specific prefix vending
	Given I have an account information such that member type is <member_type>
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I click on the UPC PREFIX button
	And I click on the ALLIANCE PREFIX button
	And I select <capacity> capacity radio button
	And I click on the SPECIFIC PREFIX button
	And I select UPC prefix from CompanyPrefix table 
	And I input selected prefix 
	And I verify Prefix Available
	And I click on the NEXT button
	And I click on VEND IDENTIFIER button on Review page
	And I wait for sync to be successful
	Then I check this prefix in CompanyPrefix table
	And I check that vended identifier matches major key in Name table with leading 0 removed
	And I check that member type is #UPC
	And I check that category is MO
	And I see a new entity GLN displayed
	And If <case> = Existing, I check that displayed GLN does not match ENTITY_GLN in Demog_All_W table
	And If <case> = New, I check that entity GLN is not created in Demog_All_W table
	And I check that account information is correct
	And I check that identifier type is UPC Prefix
	And I check that capacity is <capacity>
	And I check that UPC Range is Alliance Prefix

	@debug @light
	Examples:
		| capacity | member_type | case     |
		| 10       | CM          | Existing |
		| 100000   | HCM         | New      |
		| 100      | AO          | N/A      |

	Examples:
		| capacity | member_type |case		|
		| 10       | CM          | Existing |
		| 100      | CM          | Existing |
		| 1000     | CM          | Existing |
		| 10000    | CM          | Existing |
		| 100000   | CM          | Existing |
		| 10       | NM          | New      |
		| 100      | NM          | New      |
		| 1000     | NM          | New      |
		| 10000    | NM          | New      |
		| 100000   | NM          | New      |
		| 10       | XCM         | New      |
		| 100      | XCM         | New      |
		| 1000     | XCM         | New      |
		| 10000    | XCM         | New      |
		| 100000   | XCM         | New      |
		| 10       | HCM         | New      |
		| 100      | HCM         | New      |
		| 1000     | HCM         | New      |
		| 10000    | HCM         | New      |
		| 100000   | HCM         | New      |
		| 10       | AO          | N/A      |
		| 100      | AO          | N/A      |
		| 1000     | AO          | N/A      |
		| 10000    | AO          | N/A      |
		| 100000   | AO          | N/A      |



Scenario Outline: Vend prefix for the capacity which not match to the range predicate 
	Given I have an account information such that member type is CM, XCM, HCM, NM
	When I navigate to UIIS UI
	And I log in
	And I select Vend or Hold an Identifier menu
	And I select Vend an Identifier button
	And I input account number
	And I click on validate button
	And I observe validation passes with green check mark
	And I click on the UPC PREFIX button
	And I click on the STANDARD UPC PREFIX button
	And I select <capacity> capacity radio button
	And I click on the SPECIFIC PREFIX button
	And I select a UPC prefix from range for different capacity than <capacity>
	And I input selected prefix
	Then I see message for PREFIX NOT AVAILABLE

	@debug @light
	Examples: 
		| capacity |
		| 10       |

	Examples: 
		| capacity |
		| 10       |
		| 100      |
		| 1000     |
		| 10000    |
		| 100000   |


Scenario Outline: Open Range that is already open
	Given I have a <prefix_type> range which is already open
	When I navigate to UIIS UI
	And I log in
	And I select Open a Prefix Range menu
	And I click a RANGE button for <prefix_type>
	And I select <capacity> capacity radio button
	And I input the specific range
	And I input the reason for opening the range
	And I click on Next button in Open Prefix Range page
	And click on the OPEN RANGE button
	Then I see error on open range review page: Range cannot be opened as it is already open.

	@debug @light
	Examples:
		| capacity | prefix_type |
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
	