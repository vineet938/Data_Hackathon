Feature: Handling existing umbrella members
	Candidates can't be removed if they are already an umbrella member.
	Candidates can't be added if they are a member of a different umbrella parent.

Background: Login and open umbrella candidates manage
    Given parent company
        | id       | name                             |
        | 15753484 | Quality Supply Chain Co-op, Inc. |
    When open Data Hub Admin page
    And login
    And click manage link for Umbrella Candidates
    And enter the parent company account number and click select

Scenario: Candidates can't be removed if they are already umbrella members
	When call umbrella api to delete all members
	And call candidate api to define candidates
		| id       | name                        |
		| 15995822 | QSCC                        |
		| 10090937 | Under Armour, Inc.          |
		| 10016624 | Wendy's International, Inc. |
		| 16006053 | Wendy's QSCC                |
	And call umbrella api to define members
		| id       | name                        |
		| 15995822 | QSCC                        |
	And refresh page
	And enter the parent company account number and click select
	Then remove button for the following company is disabled
		| id       | name                        |
		| 15995822 | QSCC                        |
	And the following item is annotated with exclamation mark
		| id       | name                        |
		| 15995822 | QSCC                        |
	And remove button for the following companies are enabled
		| id       | name                        |
		| 10090937 | Under Armour, Inc.          |
		| 10016624 | Wendy's International, Inc. |
		| 16006053 | Wendy's QSCC                |
	And the following items are not annotated with exclamation mark
		| id       | name                        |
		| 10090937 | Under Armour, Inc.          |
		| 10016624 | Wendy's International, Inc. |
		| 16006053 | Wendy's QSCC                |

Scenario: Adding other parent's member should be prevented
	Given parent company
         | id       | name                 |
         | 10528392 | GS1 US Enterprises 1 |
	When refresh page
	And enter the parent company account number and click select
	And add candidates to the list
		| id       | name |
		| 15995822 | QSCC |
	Then added candidate company is not shown in the list
		

