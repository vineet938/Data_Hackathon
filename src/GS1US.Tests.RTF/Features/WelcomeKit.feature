Feature: CSA Welcome Kit
	Check if the Welcome Kit is sent correctly

Scenario Outline: New user buys a prefix and receives a Welcome Kit
	When I navigate to CSA web site
	And I skip the modal
	And I fill primary contact form using test email account
	And I check the same-as-primary-contact checkbox
	And I click on the next button on contact details page
	And I select prefixes: <prefixes>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I fill contact information using primary contact
	And I click on the license agreement checkbox
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	Then I see <count> prefixes displayed
	And I receive the Welcome Kit email

	Examples: 
		| prefixes      | count |
		| 10            | 1     |
		| 10, 100       | 2     |
		| 1000, 100, 10 | 0     |
