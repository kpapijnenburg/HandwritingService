using HandwritingService.Domain;
using HandwritingService.Logic.Abstract;
using HandwritingService.Web.Messaging.Consumers;
using HandwritingService.Web.Messaging.Messages;
using KPA.Database.Abstractions;
using Moq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HandwritingService.Tests
{
    public class NoteCreatedMessageConsumerTests
    {
        private readonly NoteCreatedMessageConsumer consumer;

        private readonly Mock<IConnection> connectionMock;
        private readonly Mock<ITextExtractor> textExtractorMock;
        private readonly Mock<IRepository<Handwriting>> repositoryMock;

        public NoteCreatedMessageConsumerTests()
        {
            connectionMock = CreateConnectionMock();
            textExtractorMock = new Mock<ITextExtractor>();
            repositoryMock = new Mock<IRepository<Handwriting>>();

            consumer = new NoteCreatedMessageConsumer(connectionMock.Object, textExtractorMock.Object, repositoryMock.Object);
        }

        private Mock<IConnection> CreateConnectionMock()
        {
            var connectionMock = new Mock<IConnection>();

            return connectionMock;
        }

        [Fact]
        public async Task ProcessMessageAsync_MessageIsNull_Returns()
        {
            // Arrange
            NoteCreatedMessage message = null;

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            textExtractorMock.Verify(t => t.FromImage(It.IsAny<byte[]>()), Times.Never);
            repositoryMock.Verify(r => r.Create(It.IsAny<Handwriting>()), Times.Never);
        }

        [Fact]
        public async Task ProcessMessageAsync_MessageImageIsNull_Returns()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = null };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            textExtractorMock.Verify(t => t.FromImage(It.IsAny<byte[]>()), Times.Never);
            repositoryMock.Verify(r => r.Create(It.IsAny<Handwriting>()), Times.Never);
        }
        [Fact]
        public async Task ProcessMessageAsync_MessageImageIsEmpty_Returns()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = new byte[0] };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            textExtractorMock.Verify(t => t.FromImage(It.IsAny<byte[]>()), Times.Never);
            repositoryMock.Verify(r => r.Create(It.IsAny<Handwriting>()), Times.Never);
        }

        [Fact]
        public async Task CreateHandwriting_SetsState_ToFinished()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = new byte[1] };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            repositoryMock.Verify(r => r.Create(It.Is<Handwriting>(h => h.State == State.Finished)));
        }

        [Fact]
        public async Task CreateHandwriting_CreatedAt_SetToDateOfToday()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = new byte[1] };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            repositoryMock.Verify(r => r.Create(It.Is<Handwriting>(h => h.CreatedAt.Date == DateTime.Today.Date)));
        }

        [Fact]
        public async Task CreateHandwriting_UpdatedAt_SetToDateOfToday()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = new byte[1] };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            repositoryMock.Verify(r => r.Create(It.Is<Handwriting>(h => h.UpdatedAt.Date == DateTime.Today.Date)));
        }

        [Fact]
        public async Task CreateHandwriting_NoteId_SetToMessageNoteId()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = new byte[1] };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            repositoryMock.Verify(r => r.Create(It.Is<Handwriting>(h => h.NoteId == message.NoteId)));
        }
        
        [Fact]
        public async Task CreateHandwriting_Image_SetToMessageImage()
        {
            // Arrange
            var message = new NoteCreatedMessage() { NoteId = 1, Image = new byte[1] };

            // Act
            await consumer.ProcessMessageAsync(message);

            // Assert
            repositoryMock.Verify(r => r.Create(It.Is<Handwriting>(h => h.Image == message.Image)));
        }
    }
}
