using BIED.Messaging.Abstractions;
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
        private readonly Mock<IMessageProducer> messageProducerMock;

        public HandwritingControllerTests()
        {
            repositoryMock = new Mock<IRepository<Handwriting>>();
            controller = new HandwritingController(repositoryMock.Object, messageProducerMock.Object);
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

        [Fact]
        public async Task Create_HandwritingWasCreated_ReturnsCreatedResult()
        {
            var expected = new Handwriting() { Id = 1 };

            repositoryMock.Setup(r => r.Create(It.IsAny<Handwriting>()))
                .Returns(Task.FromResult(expected));

            // Act
            var result = await controller.Create(new Handwriting()) as CreatedResult;

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_HandwritingWasCreated_ReturnsObjectLocation()
        {
            // Arrange
            var expected = new Handwriting() { Id = 1 };

            repositoryMock
                .Setup(r => r.Create(It.IsAny<Handwriting>()))
                .Returns(Task.FromResult(expected));

            //Act
            var result = await controller.Create(new Handwriting()) as CreatedResult;

            // Assert
            Assert.Equal("/api/handwriting/1", result.Location);
        }

        [Fact]
        public async Task Create_HandwritingWasCreated_ReturnsObjectValue()
        {
            var expected = new Handwriting() { Id = 1 };

            repositoryMock
                .Setup(r => r.Create(It.IsAny<Handwriting>()))
                .Returns(Task.FromResult(expected));

            //Act
            var result = await controller.Create(new Handwriting()) as CreatedResult;

            // Assert
            Assert.Equal(expected, result.Value);

        }

        [Fact]
        public async Task Update_NoHandwritingExists_ReturnsNotFound()
        {
            // Arrange
            Handwriting expected = null;

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            // Act
            var result = await controller.Update(1, new Handwriting());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_HandwritingUpdated_ReturnsNoContent()
        {
            // Arrange
            var expected = new Handwriting() { Id = 1 };

            repositoryMock
                .Setup(r => r.Update(It.IsAny<Handwriting>()))
                .Returns(Task.FromResult(expected));

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            // Act
            var result = await controller.Update(1, new Handwriting()) as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_HandwritingWasNotFound_ReturnsNotFoundResult()
        {
            Handwriting expected = null;

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            // Act
            var result = await controller.Delete(1, new Handwriting());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_HandwritingWasDeleted_ReturnsNoContentResult()
        {
            // Arrange
            var expected = new Handwriting() { Id = 1 };

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            repositoryMock
                .Setup(r => r.Delete(It.IsAny<Handwriting>()));

            // Act
            var result = await controller.Delete(1, new Handwriting()) as NoContentResult;

            //
            Assert.IsType<NoContentResult>(result);
        }
    }
}
