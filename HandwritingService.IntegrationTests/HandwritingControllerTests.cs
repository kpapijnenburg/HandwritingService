using HandwritingService.Domain;
using KPA.Database.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HandwritingService.IntegrationTests
{
    public class HandwritingControllerTests : IClassFixture<HandwritingServiceFactory>
    {
        private readonly HttpClient client;

        public HandwritingControllerTests()
        {
            var factory = new HandwritingServiceFactory();
            client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_HandwritingExists_Returns200Ok()
        {
            var response = await client.GetAsync("/api/handwriting");

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async Task GetById_HandwritingDoesNotExist_Returns404NotFound()
        {
            var response = await client.GetAsync("/api/handwriting/255");

            Assert.Equal(404, (int)response.StatusCode);
        }

        [Fact]
        public async Task GetById_HandwritingDoesExist_Returns200Ok()
        {
            var response = await client.GetAsync("/api/handwriting/1");

            Assert.Equal(200, (int)response.StatusCode);
        }
    }
}
