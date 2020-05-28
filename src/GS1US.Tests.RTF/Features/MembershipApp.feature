Feature: Request prefixes from Membership Application

Scenario Outline: Existing customer buys prefixes using PayPal
	When I navigate to Membership Application site
	And I login to GS1 US User Portal using test account
	And I select my test company from company dropdown and signin
	# Contact forms should be filled already
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I query contact info for test account
	And I fill contact information using primary contact
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	Then I see <n> prefixes displayed
	And In Name table, following entries are created or updated
		| MemberType | Count |
		| CM         | 1     |
		| I          | 0     |
		| #UPC       | <upc> |
	And Subscription entries are created as follows
		| ProductCode  | Count |
		| PC_AN_CAP_A  | <upc> |
		| PC_AN_CAP_A1 | <upc> |
	And If prorated, I should see <upc> PC_AN_CAP_A2 entries
	And In Subscriptions table, billing amounts are correct
	And In Trans table, payments are added to a batch

	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |
		| 100                                        | 1   | 1 |
		| 1000                                       | 1   | 1 |
		| 10000                                      | 1   | 1 |
		| 100000                                     | 1   | 1 |
		| 10, 10                                     | 2   | 2 |
		| 100, 1000                                  | 2   | 2 |
		| 10, 100, 1000                              | 3   | 0 |
		| 100000, 10000, 10000, 10000, 1000, 100, 10 | 7   | 0 |
		| 10,10000,10,100000,1000,10,100,10,10       | 9   | 0 |
