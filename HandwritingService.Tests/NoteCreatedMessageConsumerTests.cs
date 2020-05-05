using BIED.Messaging;
using BIED.Messaging.Abstractions;
using HandwritingService.Domain;
using HandwritingService.Logic.Abstract;
using HandwritingService.Web.Messaging.Consumers;
using HandwritingService.Web.Messaging.Messages;
using KPA.Database.Abstractions;
using Moq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
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
            connectionMock = new Mock<IConnection>();
            textExtractorMock = new Mock<ITextExtractor>();
            repositoryMock = new Mock<IRepository<Handwriting>>();

            this.consumer = new NoteCreatedMessageConsumer(connectionMock.Object, textExtractorMock.Object, repositoryMock.Object);

        }

        [Fact]
        public Task Test_Test()
        {
        }
    }
}
