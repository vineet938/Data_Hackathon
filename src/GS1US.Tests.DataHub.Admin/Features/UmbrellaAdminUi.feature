Feature: Items can be added and removed to/from the candidates list

Background: Login and open umbrella candidates manage
    Given parent company
        | id       | name                             |
        | 15753484 | Quality Supply Chain Co-op, Inc. |
    When open Data Hub Admin page
    And login
    And click manage link for Umbrella Candidates
    And enter the parent company account number and click select

Scenario: No candidates
    When call umbrella api to delete all members
    And call candidate api to delete all candidates
    And refresh page
	And enter the parent company account number and click select
    Then the candidate list is empty

Scenario: Add one candidate
	When add candidates to the list
        | id       | name |
        | 15995822 | QSCC |
    And click save button
    Then added candidate company is shown in the list
    And added candidate company is returned in get api call

Scenario: Add multiple candidates
    When call umbrella api to delete all members
	And refresh page
	And enter the parent company account number and click select
    When add candidates to the list
        | id        | name                        |
        | 10090937  | Under Armour, Inc.          |
        | 10016624  | Wendy's International, Inc. |
        | 16006053  | Wendy's QSCC                |
    And click save button
    Then added candidate companies are shown in the list
    And added candidate companies are returned in get api call

Scenario: Remove one company
    When remove companies from the list
        | id        | name |
        | 15995822  | QSCC |
    And click save button
    Then removed candidate company is not shown in the list
    And removed candidate company is not returned in get api call

Scenario: Remove and add companies
    When remove companies from the list
        | id        | name                        |
        | 10090937  | Under Armour, Inc.          |
        | 10016624  | Wendy's International, Inc. |
    And add candidates to the list
        | id       | name |
        | 15995822 | QSCC |
    And click save button
    Then added candidate companies are shown in the list
    And added candidate companies are returned in get api call
    And removed candidate companies are not shown in the list
    And removed candidate companies are not returned in get api call

Scenario: Remove all
    When remove all candidates
    And click save button
    Then candidate list has no items
    And get candidates api call returns 0 items

