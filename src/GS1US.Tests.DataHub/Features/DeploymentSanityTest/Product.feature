Feature: Product sanity checks

Scenario Outline: Company Permissions
	Given the company name: <company_name>
	Then check the company has permissions: <permissions>

	Examples:
		| company_name                    | permissions                               |
		| Amalgamated Hospital Purchasing | Pool, L-VUCMEX, P_VUEX                    |
		| Medical Devices R Us            | Prefix, C-VULMEX, L-VUCM, P-VUCM, All API |
		| GS1 US Enterprises 1            | Prefix, C-VULMEX, L-VUCM, P-VUCM, All API |


@vsts
Scenario: Product 1
	When login as Medical Devices R Us
	And wait and close Walk Me Through popup
	And create a product: P1
	And change status of product P1 to In Use
	And share the product P1 with GS1 US Enterprises 1
	And export products and check exported product count

@vsts
Scenario: Product 2
	When login as GS1 US Enterprises 1
	And wait and close Walk Me Through popup
	And view the product P1 shared by Medical Devices R Us
	And do a product import and check imported product count
	And export products and check exported product count
	#And verify breadcrumbs show correctly

@vsts
Scenario: Product 3
	When login as Medical Devices R Us
	And wait and close Walk Me Through popup
	And change status of product P1 to Archived
