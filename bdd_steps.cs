using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow; 

namespace YourNamespace
{
    [Binding]
    public class PetstoreSteps
    {
        private RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        private List<User> users;

        [Given(@"I have the following user information:")]
        public void GivenIHaveTheFollowingUserInformation(Table table)
        {
            users = new List<User>();
            foreach (var row in table.Rows)
            {
                users.Add(new User
                {
                    id = int.Parse(row["id"]),
                    username = row["username"],
                    firstName = row["firstName"],
                    lastName = row["lastName"],
                    email = row["email"],
                    password = row["password"],
                    phone = row["phone"],
                    userStatus = int.Parse(row["userStatus"])
                });
            }
        }

        [When(@"I send a POST request to ""(.*)""")]
        public void WhenISendAPOSTRequestTo(string endpoint)
        {
            restClient = new RestClient(endpoint);
            restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(users);

            restResponse = restClient.Execute(restRequest);
        }

        [Then(@"the response status code should be (\d+)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)restResponse.StatusCode);
        }

        [Given(@"I have created a user with username ""(.*)""")]
        public void GivenIHaveCreatedAUserWithUsername(string username)
        {
            // User creation steps can be implemented here, such as calling the POST endpoint with the given username or retrieving an existing user
        }

        [When(@"I send a GET request to ""(.*)""")]
        public void WhenISendAGETRequestTo(string endpoint)
        {
            restClient = new RestClient(endpoint);
            restRequest = new RestRequest(Method.GET);

            restResponse = restClient.Execute(restRequest);
        }

        [Then(@"the response should contain the following user information:")]
        public void ThenTheResponseShouldContainTheFollowingUserInformation(Table table)
        {
            var expectedUser = new User();
            foreach (var row in table.Rows)
            {
                expectedUser = new User
                {
                    id = int.Parse(row["id"]),
                    username = row["username"],
                    firstName = row["firstName"],
                    lastName = row["lastName"],
                    email = row["email"],
                    password = row["password"],
                    phone = row["phone"],
                    userStatus = int.Parse(row["userStatus"])
                };
            }

            var actualUser = JsonConvert.DeserializeObject<User>(restResponse.Content);
            Assert.AreEqual(expectedUser.id, actualUser.id);
            Assert.AreEqual(expectedUser.username, actualUser.username);
            Assert.AreEqual(expectedUser.firstName, actualUser.firstName);
            Assert.AreEqual(expectedUser.lastName, actualUser.lastName);
            Assert.AreEqual(expectedUser.email, actualUser.email);
            Assert.AreEqual(expectedUser.password, actualUser.password);
            Assert.AreEqual(expectedUser.phone, actualUser.phone);
            Assert.AreEqual(expectedUser.userStatus, actualUser.userStatus);
        }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public int userStatus { get; set; }
    }
}