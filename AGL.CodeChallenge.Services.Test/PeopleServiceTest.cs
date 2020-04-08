using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AGL.CodeChallenge.Common.Configuration;
using AGL.CodeChallenge.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AGL.CodeChallenge.Services.Test
{
    public class PeopleServiceTest
    {
        private AppSettings appSettings = new AppSettings() { AGLDeveloperTest = new AGLDeveloperTest() { Url = "http://agl-api-demo.com" } };

        [Fact]
        public async Task GivenSuccessResponseFromServer_WhenPersonRequests_ThenPersonsAreReturned()
        {
            // Arrange
            var response = @"[{
              ""name"": ""Bob"",
              ""gender"": ""Male"",
              ""age"": 23,
              ""pets"":[{""name"":""Garfield"",""type"":""Cat""},{""name"":""Fido"",""type"":""Dog""}]
            },
            {
              ""name"": ""Jennifer"",
              ""gender"": ""Female"",
              ""age"": 18,
              ""pets"":[{""name"":""Garfield"",""type"":""Cat""}]
            }]";

            var logger = Mock.Of<ILogger<PeopleService>>();
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.OK);
            var httpClient = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri(appSettings.AGLDeveloperTest.Url)
            };
            var peopleService = new PeopleService(httpClient, logger, appSettings);

            // Act
            var result = await peopleService.GetPersonPetsAsync();

            // Assert
            result.Count.Equals(2);
        }

        [Fact]
        public async Task GivenSuccessResponseFromServer_WhenPersonRequests_ThenPersonsAreNotReturned()
        {
            // Arrange
            var response = @"[]";

            var logger = Mock.Of<ILogger<PeopleService>>();
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.BadRequest);
            var httpClient = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri(appSettings.AGLDeveloperTest.Url)
            };
            var peopleService = new PeopleService(httpClient, logger, appSettings);

            // Act
            var result = await peopleService.GetPersonPetsAsync();

            // Assert
            Assert.Null(result);
        }
    }
}
