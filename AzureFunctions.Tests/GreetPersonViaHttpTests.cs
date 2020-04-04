using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Azure_functions;
using AzureFunctions.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AzureFunctions.Tests
{
    public class GreetPersonViaHttpTests
    {
        private TestPersonRepository personRepository;
        private readonly ILogger logger = new Mock<ILogger>().Object;
        private GreetPersonViaHttp sut;

        public GreetPersonViaHttpTests()
        {
            this.personRepository = new TestPersonRepository();
            this.sut = new GreetPersonViaHttp(this.personRepository);
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("Not a number")]
        public async Task Run_QueryStringValueNotSetCorrectly_ReturnsBadRequest(string id)
        {
            var request = new TestHttpRequest();

            if (id != null)
            {
                request.QueryDictionary.Add("id", id);
            }

            var result = await this.sut.Run(new TestHttpRequest(), this.logger);

            result
                .Should()
                .Match<ObjectResult>(r => r.StatusCode == (int)HttpStatusCode.Forbidden)
                .And
                .Match<ObjectResult>(r => r.Value.ToString() == "Identify yourself!");
        }

        [Fact]
        public async Task Run_PersonNotFound_ReturnsNotFound()
        {
            var request = new TestHttpRequest
            {
                QueryDictionary = { ["id"] = "999999" }
            };

            var result = await this.sut.Run(request, this.logger);

            result
                .Should()
                .Match<NotFoundObjectResult>(r => r.StatusCode == (int)HttpStatusCode.NotFound)
                .And
                .Match<NotFoundObjectResult>(r => r.Value.ToString() == "I don't know you!");
        }

        [Fact]
        public async Task Run_PersonFound_ReturnsGreeting()
        {
            var request = new TestHttpRequest
            {
                QueryDictionary = { ["id"] = "2" }
            };

            var result = await this.sut.Run(request, this.logger);

            result
                .Should()
                .Match<OkObjectResult>(r => r.StatusCode == (int)HttpStatusCode.OK)
                .And
                .Match<OkObjectResult>(r => r.Value.ToString() == "Hello, Fake John Smith 2!");
        }
    }
}