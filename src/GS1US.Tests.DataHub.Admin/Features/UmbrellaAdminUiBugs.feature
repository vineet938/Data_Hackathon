Feature: Regression tests for found bugs

Background: Login and open umbrella candidates manage
    Given parent company
        | id       | name                             |
        | 15753484 | Quality Supply Chain Co-op, Inc. |
    When open Data Hub Admin page
    And login
    And click manage link for Umbrella Candidates
    And enter the parent company account number and click select

Scenario: Check discard changes button works
	When call umbrella api to delete all members
	And call candidate api to define candidates
		| id       | name                        |
		| 15995822 | QSCC                        |
		| 10090937 | Under Armour, Inc.          |
		| 10016624 | Wendy's International, Inc. |
		| 16006053 | Wendy's QSCC                |
	And refresh page
	And enter the parent company account number and click select
	When remove companies from the list
		| id       | name                        |
		| 15995822 | QSCC                        |
	And discard changes
	And remove companies from the list
		| id       | name                        |
		| 15995822 | QSCC                        |
		| 10090937 | Under Armour, Inc.          |
	And discard changes
	Then removed candidate companies are shown in the list