Feature: Common Signup App

@paypal
Scenario Outline: New customer buys prefixes with PayPal
	When I navigate to CSA web site
	And I skip the modal
	And I fill primary contact form
	And I update primary contact email using test email account
	And I check the same-as-primary-contact checkbox
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created
		| MemberType | Count |
		| CM         | 1     |
		| I          | 1     |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count  |
		| PC_AN_CAP    | 1      |
		| PC_AN_CAP1   | 1      |
	And In Subscriptions table, billing amounts are correct
	And In IDM database, company, user and claims are created correctly
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@vsts
	Examples:
		| capacities                                 | upc | upc2 | n |
		| 10                                         | 1   | 0    | 1 |

	@light
	Examples: 
		| capacities                                 | upc | upc2 | n |
		| 10                                         | 1   | 0    | 1 |
		| 100, 1000                                  | 2   | 1    | 2 |
		| 10, 100, 1000                              | 3   | 2    | 0 |

	@heavy
	Examples: 
		| capacities                                 | upc | upc2 | n |
		| 10                                         | 1   | 0    | 1 |
		| 100                                        | 1   | 0    | 1 |
		| 1000                                       | 1   | 0    | 1 |
		| 10000                                      | 1   | 0    | 1 |
		| 100000                                     | 1   | 0    | 1 |
		| 10, 10                                     | 2   | 1    | 2 |
		| 100, 1000                                  | 2   | 1    | 2 |
		| 10, 100, 1000                              | 3   | 2    | 0 |
		| 100000, 10000, 10000, 10000, 1000, 100, 10 | 7   | 6    | 0 |
		| 10,10000,10,100000,1000,10,100,10,10       | 9   | 8    | 0 |


@paypal
Scenario Outline: New customer buys prefixes with PayPal - Different executive contact
	When I navigate to CSA web site
	And I skip the modal
	And I fill primary contact form
	And I update primary contact email using test email account
	And I fill executive contact form
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created
		| MemberType | Count |
		| CM         | 1     |
		| I          | 2     |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count  |
		| PC_AN_CAP    | 1      |
		| PC_AN_CAP1   | 1      |
	And In Subscriptions table, billing amounts are correct
	And In IDM database, company, user and claims are created correctly
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@light
	Examples: 
		| capacities                                 | upc | n |
		| 100                                        | 1   | 1 |
		| 10, 10                                     | 2   | 2 |
		| 10, 100, 1000                              | 3   | 0 |

	@heavy
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




@paypal
Scenario Outline: Existing customer buys prefixes with PayPal - Proration
	When I navigate to CSA web site
	And I select an account with proration
	And I enter account number and zip code and submit
	And I fill primary contact form with modification
	And I fill executive contact form with modification
	And I update primary contact email using test email account
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created or updated
		| MemberType | Count |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count |
		| PC_AN_CAP_A  | <upc> |
		| PC_AN_CAP_A1 | <upc> |
		| PC_AN_CAP_A2 | <upc> |
	And In Subscriptions table, billing amounts are correct
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@debug
	Examples:
		| capacities                                 | upc | n |
		| 10, 10                                     | 2   | 2 |
		| 100, 1000                                  | 2   | 2 |
		| 10, 100, 1000                              | 3   | 0 |
		| 100000, 10000, 10000, 10000, 1000, 100, 10 | 7   | 0 |
		| 10,10000,10,100000,1000,10,100,10,10       | 9   | 0 |

	@light
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |
		| 10, 10                                     | 2   | 2 |
		| 10, 100, 1000                              | 3   | 0 |

	@heavy
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


@paypal
Scenario Outline: Existing customer buys prefixes with PayPal - No proration
	When I navigate to CSA web site
	And I select an account without proration
	And I enter account number and zip code and submit
	And I fill primary contact form with modification
	And I fill executive contact form with modification
	And I update primary contact email using test email account
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I click on the PayPal button
	And I proceed with PayPal payment
	And I confirm the payment
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created or updated
		| MemberType | Count |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count |
		| PC_AN_CAP_A  | <upc> |
		| PC_AN_CAP_A1 | <upc> |
	And In Subscriptions table, billing amounts are correct
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@debug
	Examples:
		| capacities                                 | upc | n |
		| 10, 10                                     | 2   | 2 |
		| 100, 1000                                  | 2   | 2 |
		| 10, 100, 1000                              | 3   | 0 |
		| 100000, 10000, 10000, 10000, 1000, 100, 10 | 7   | 0 |
		| 10,10000,10,100000,1000,10,100,10,10       | 9   | 0 |

	@light
	Examples: 
		| capacities                                 | upc | n |
		| 100000                                     | 1   | 1 |
		| 100, 1000                                  | 2   | 2 |
		| 10, 100, 1000                              | 3   | 0 |

	@heavy
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


@cc
Scenario Outline: New customer buys prefixes with CC
	When I navigate to CSA web site
	And I skip the modal
	And I fill primary contact form
	And I update primary contact email using test email account
	And I check the same-as-primary-contact checkbox
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I fill CC payment information and submit
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created
		| MemberType | Count |
		| CM         | 1     |
		| I          | 1     |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count  |
		| PC_AN_CAP    | 1      |
		| PC_AN_CAP1   | 1      |
	And In Subscriptions table, billing amounts are correct
	And In IDM database, company, user and claims are created correctly
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@light
	Examples: 
		| capacities                                 | upc | upc2 | n |
		| 10                                         | 1   | 0    | 1 |

	@heavy
	Examples: 
		| capacities                                 | upc | upc2 | n |
		| 10                                         | 1   | 0    | 1 |
		| 10, 10                                     | 2   | 1    | 2 |


@cc
Scenario Outline: New customer buys prefixes with CC - Different executive contact
	When I navigate to CSA web site
	And I skip the modal
	And I fill primary contact form
	And I update primary contact email using test email account
	And I fill executive contact form
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I fill CC payment information and submit
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created
		| MemberType | Count |
		| CM         | 1     |
		| I          | 2     |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count  |
		| PC_AN_CAP    | 1      |
		| PC_AN_CAP1   | 1      |
	And In Subscriptions table, billing amounts are correct
	And In IDM database, company, user and claims are created correctly
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@light
	Examples: 
		| capacities                                 | upc | n |
		| 10, 10                                     | 2   | 2 |

	@heavy
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |
		| 10, 10                                     | 2   | 2 |




@cc
Scenario Outline: Existing customer buys prefixes with CC - Proration
	When I navigate to CSA web site
	And I select an account with proration
	And I enter account number and zip code and submit
	And I fill primary contact form with modification
	And I fill executive contact form with modification
	And I update primary contact email using test email account
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I fill CC payment information and submit
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created or updated
		| MemberType | Count |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count |
		| PC_AN_CAP_A  | <upc> |
		| PC_AN_CAP_A1 | <upc> |
		| PC_AN_CAP_A2 | <upc> |
	And In Subscriptions table, billing amounts are correct
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@vsts2
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |

	@debug
	Examples: 
		| capacities                                 | upc | n |
		| 10, 10                                     | 2   | 2 |

	@light
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |

	@heavy
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |
		| 10, 10                                     | 2   | 2 |


@cc
Scenario Outline: Existing customer buys prefixes with CC - No proration
	When I navigate to CSA web site
	And I select an account without proration
	And I enter account number and zip code and submit
	And I fill primary contact form with modification
	And I fill executive contact form with modification
	And I update primary contact email using test email account
	And I click on the next button on contact details page
	And I select prefixes: <capacities>
	And I click on the policy consent checkbox
	And I click on the next button on program details page
	And I click on the license agreement checkbox
	And I fill contact information using primary contact
	And I fill CC payment information and submit
	And I wait for logic app to finish
	Then I see <n> prefixes displayed
	And In Name table, following entries are created or updated
		| MemberType | Count |
		| #UPC       | <upc> |
	And Relationship entries have correct target ID
	And Subscription entries are created as follows
		| ProductCode  | Count |
		| PC_AN_CAP_A  | <upc> |
		| PC_AN_CAP_A1 | <upc> |
	And In Subscriptions table, billing amounts are correct
	And I receive the Welcome Kit email
	And In Trans table, payments are added to a batch

	@debug
	Examples: 
		| capacities                                 | upc | n |
		| 10, 10                                     | 2   | 2 |

	@light
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |

	@heavy
	Examples: 
		| capacities                                 | upc | n |
		| 10                                         | 1   | 1 |
		| 10, 10                                     | 2   | 2 |
