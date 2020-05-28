Feature: Datahub Member Signup

Scenario Outline: Updating account from member datahub signup portal by using PayPal
	When I navigate to Datahub Signup Portal
	And I login to GS1 US User Portal using test account
	And I select my test company from company dropdown and signin

	And I take a note of first and last name
	And I click on the next button on contact details page

	And I select number of users for create/manage products: <create product>
	And I select number of users for view/use product data: <access product>
	And I select number of users for create/manage locations: <create location>
	And I select number of users for view/use location data: <access location>
	And I select number of users for view/use company/prefix data: <access company>

	And I click on the next button on datahub signup product details page

	And I set API checkbox on addons page: <api>
	And I click on the next button on the datahub signup addons page

	And I scroll to bottom of the data hub agreement text
	And I click on the data hub agreement checkbox
	And I fill in the data hub agreement form
	And I click on the submit button on data hub agreement
	And I click on the PayPal button on data hub signup payment page
	And I proceed with PayPal payment
	And I confirm the payment
	Then I see the Thank You page for data hub signup

	Examples: 
		| create product | access product | create location | access location | access company | api     |
		| 5              | 0              | 0               | 0               | 0              | uncheck |
		| 5              | 1              | 0               | 0               | 0              | check   |
		| 5              | 1              | 5               | 0               | 0              | uncheck |