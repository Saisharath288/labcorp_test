Feature: Test 
  Scenario: Create a user with array
    Given I have the following user information:
      | id    | username      | firstName | lastName | email             | password | phone    | userStatus |
      | 1 | abc1234   | Sharath      | LLL      | sharath@gmail.com      | V1234  | 8764  | 0         

    When I send a POST request to "https://petstore.swagger.io/v2/user/createWithArray"

    Then the response status code should be 200

  Scenario: Retrieve user information
    Given I have created a user with username "323243431qw"

    When I send a GET request to "https://petstore.swagger.io/v2/user/{username}"

    Then the response status code should be 200
    And the response should contain the following user information:
      | id    | username      | firstName | lastName | email             | password | phone    | userStatus |
      | 1 | abc1234   | Sharath      | LLL      | sharath@gmail.com      | V1234  | 8764  | 0   