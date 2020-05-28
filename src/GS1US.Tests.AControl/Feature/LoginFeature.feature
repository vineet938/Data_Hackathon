Feature: LoginFeature

Scenario: Login Access Control
	When I go to the Access Control website
	And click on the Login button
	And enter user credentials for Access Control
	Then I verify that I'm logged in
