Feature: Company sanity checks

@vsts
Scenario: Company 1
	When login as GS1 US Enterprises 1
	And wait and close Walk Me Through popup
	And verify prefix search result
		| key       | company name       |
		| 894514002 | B. Happybags, LLC. |
	And verify prefix search result
		| key       | company name             |
		| 180253000 | Papaya- Creative Abandon |
	And verify GTIN search result
		| key            | company name          |
		| 60653890000005 | Gorilla Snot USA, LLC |
	And verify GTIN search result
		| key            | company name |
		| 05012345000015 | GS1 UK Ltd   |
	And verify GTIN search result
		| key           | company name           |
		| 9300658000003 | LD&D AUSTRALIA PTY LTD |
	And verify GLN search result
		| key           | company name          |
		| 0633922000003 | Eagle Cap Nursery LLC |
	And verify GLN search result
		| key           | company name   |
		| 4399901110991 | "ALter Imbiss" |
	And download and verify active US prefix list
	And in List Match, check GTIN list upload works


Scenario Outline: Company Prefix Search
	When login as GS1 US Enterprises 1
	And wait and close Walk Me Through popup
	And verify prefix search result
		| key      | company name   |
		| <prefix> | <company name> |

@source:CompanySearchExamples.xlsx:Prefix
Examples:
	| prefix | company name |


Scenario Outline: Company GTIN Search
	When login as GS1 US Enterprises 1
	And wait and close Walk Me Through popup
	And verify GTIN search result
		| key    | company name   |
		| <gtin> | <company name> |

@source:CompanySearchExamples.xlsx:GTIN
Examples:
	| gtin | company name |
	

	