Feature: Location sanity checks

@vsts
Scenario: Location 1
	When login as GS1 US Enterprises 1
	And wait and close Walk Me Through popup
	And create a location L1
		| key             | value                                    |
		| parent          | GS1 US Enterprises 1                     |
		| industry        | Healthcare                               |
		| sc_role         | Provider                                 |
		| address1        | 1000 Lenox Dr                            |
		| city            | Lawrence Township                        |
		| state           | New Jersey                               |
		| zip             | 08648                                    |
		| phone           | (609) 620-0200                           |
		| location_type   | Org Entity                               |
		| corporate_rel   | Affiliated                               |
		| class_of_trade1 | City/County (CC)                         |
		| class_of_trade2 | Pharmacy (Inpatient and Outpatient) (PH) |
		| class_of_trade3 | Mail order pharmacy (PM)                 |
	And share location L1 with Amalgamated Hospital Purchasing
	And do a location import and check imported location count
	And export location hierarchy and verify
	And open location view/use page
	And export locations from current View/Use and verify
	And download all healthcare locations and verify
	And run location share report for L1


@vsts
Scenario: Location 2
	When login as Amalgamated Hospital Purchasing
	And wait and close Walk Me Through popup
	And view location L1
	And search location L1 from GLN index
	And export location hierarchy and verify
	And open location view/use page
	And export locations from current View/Use with MyLocations
	And open location view/use page
	And uncheck MyLocations from current location view/use page
	And export locations from current View/Use without MyLocations
	And open location view/use page
	And search Healthcare industry from current view/use page
	And clear filter from current View/Use
	And check that MyLocations is checked
	And export locations from current View/Use with MyLocations
	And send message M1 via shared location L1

@vsts
Scenario: Location 3
	When login as GS1 US Enterprises 1
	And wait and close Walk Me Through popup
	And check message M1 in inbox
	And make active location L1 to inactive

Scenario: Location 4
	When check message M1 in test email account
