Feature: UNSPSC Membership App

Scenario Outline: New customer buys prefixes from UNSPSC
	When I navigate to UNSPSC membership site
	And I fill primary contact form
	And I check the same-as-primary-contact checkbox
	And I click on the next button on contact details page
	And I select membership type <membership type> and click next
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	Then I see the UNSPSC Thank You page with no prefix

	Examples: 
		| membership type                          |
		| Corporate Global                         |
		| Corporate Plus                           |
		| Corporate                                |
		| Corporate Individual                     |
		| Education/Charity/Religious Organization |
		| Government - National/State              |
		| Government - Local                       |
		| Trade/Standards Organization             |
		| Student/Educator/Researcher              |
		| Solution Resources                       |
