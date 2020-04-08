using System;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Collections;
using AGL.CodeChallenge.Services;
using System.Collections.Generic;
using AGL.CodeChallenge.Common.Models;
using System.Threading.Tasks;
using System.IO;

namespace AGL.CodeChallenge.ConsoleApp.Test
{
    public class CatsAlphabeticalOrderTest
    {
        private Mock<ILogger<CatsAlphabeticalOrder>> logger;
        private Mock<IPeopleService> mockPeopleService;
        private CatsAlphabeticalOrder catsAlphabeticalOrder;
        private StringWriter output;
        private StringReader input;

        public CatsAlphabeticalOrderTest()
        {
            logger = new Mock<ILogger<CatsAlphabeticalOrder>>();
            mockPeopleService = new Mock<IPeopleService>();
            output = new StringWriter();
            input = new StringReader("s");
            catsAlphabeticalOrder = new CatsAlphabeticalOrder(mockPeopleService.Object, logger.Object,input,output);
        }

        [Fact]
        public void GivenSuccessResponseFromService_WhenPersonRequests_ThenPersonsPetsAreReturnedInCorrectFormat()
        {
            // Arrange
            var peopleList = new List<Person>
            {
                new Person() { Name= "Bob", Age=23, Gender="Male", Pets=new List<Pet>(){ new Pet() { Name= "Garfield", Type="Cat" } } },
                new Person() { Name= "Jennifer", Age=18, Gender="Female" , Pets=new List<Pet>(){ new Pet() { Name= "Garfield", Type="Cat" } } }
            };

            mockPeopleService.Setup(m => m.GetPersonPetsAsync()).Returns(Task.FromResult(peopleList));

            // Act
            catsAlphabeticalOrder.GetPersonAndPetsAsync().Wait();
            var result = output.ToString();

            // Assert
            var expected = "Male" + Environment.NewLine
                + " * " + "Garfield" + Environment.NewLine
                + Environment.NewLine
                + "Female" + Environment.NewLine
                + " * " + "Garfield" + Environment.NewLine
                + Environment.NewLine;

            Assert.Equal(result, expected);
        }
    }
}
