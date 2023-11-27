Feature: ApiExample

﻿Feature: Api Test Example
	Test project for API testing

@PassExample
Scenario: I am able to get a list of clients
	Given I have an endpoint api/client
	When I send a GET request
	Then I will get a 200 response
	And the expected reponse is returned

