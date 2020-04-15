using HandwritingService.Domain;
using HandwritingService.Web.Controllers;
using KPA.Database.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HandwritingService.Tests
{
    public class HandwritingControllerTests
    {
        private readonly HandwritingController controller;
        private readonly Mock<IRepository<Handwriting>> repositoryMock;

        public HandwritingControllerTests()
        {
            repositoryMock = new Mock<IRepository<Handwriting>>();
            controller = new HandwritingController(repositoryMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult()
        {
            // Arrange
            var expected = new[] { new Handwriting() }.ToList();
            repositoryMock.Setup(r => r.GetAll()).Returns(Task.FromResult(expected));

            // Act
            var result = (await controller.Get()) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsListOfHandwritings()
        {
            // Arrange
            var expected = new[] { new Handwriting() }.ToList();
            repositoryMock.Setup(r => r.GetAll()).Returns(Task.FromResult(expected));

            // Act
            var result = (await controller.Get()) as OkObjectResult;

            // Assert
            Assert.IsType<List<Handwriting>>(result.Value);
        }

        [Fact]
        public async Task GetById_HandwritingDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult<Handwriting>(null));

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_HandwritingExists_ReturnsObjectResult()
        {
            // Arrange
            var expected = new Handwriting();
            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            // Act
            var result = await controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_HandwritingExists_ReturnsHandwriting()
        {
            // Arrange
            var expected = new Handwriting();
            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            // Act
            var result = await controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.IsType<Handwriting>(result.Value);
        }
    }
}
