Feature: UmbrellaUi

Background: Login
	When login as Quality Supply Chain Co-op, Inc.
	And umbrella is deleted
	And umbrella is deleted: 10090937
	And go to manage company settings in administration menu
	And check umbrella tab is visible

Scenario: When umbrella does not exist, right pane is empty
	Then right pane of umbrella UI is empty
	And content of the left pane is correct
	And add button is diabled

Scenario: Add all children and save
	When add all umbrella children
	And save umbrella definition changes
	Then left pane of umbrella UI is empty
	And response of get umbrella call reflects the changes
	And add button is diabled

Scenario: All all button
	When click add all button
	Then select all checkbox is checked
	And all candidate checkboxes are checked
	And add button is enabled

Scenario: Add some children and save
	When add some umbrella children
	And save umbrella definition changes
	Then response of get umbrella call reflects the changes
	And content of the left pane is correct

Scenario: Nested umbrellas
	When umbrella is created as follows
		| key      | value    |
		| parent   | 10090937 |
		| children | 16006053 |
	And page is refreshed
	And click add all button
	Then there is an unselected candidate: 10090937
	And reported number of selected candidates is correct

Scenario: Test remove 1
	When add all umbrella children
	And save umbrella definition changes
	Then add button is diabled
	And remove button is disabled

Scenario: Test remove 2
	When add all umbrella children
	And save umbrella definition changes
	And select some umbrella children
	And click remove button
	And save umbrella definition changes
	Then response of get umbrella call reflects the changes
	And content of the left pane is correct

