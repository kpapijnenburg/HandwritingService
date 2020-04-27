using BIED.Messaging;
using BIED.Messaging.Abstractions;
using HandwritingService.Logic.Abstract;
using HandwritingService.Web.Messaging.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandwritingService.Web.Messaging.Consumers
{
    public class NoteCreatedMessageConsumer : MessageConsumer<NoteCreatedMessage>
    {
        private IMessageProducer MessageProducer { get; set; }
        public ITextExtractor TextExtractor { get; set; }

        public NoteCreatedMessageConsumer(IConnection connection, IMessageProducer messageProducer, ITextExtractor textExtractor) : base(connection, "notes.created", "")
        {
            MessageProducer = messageProducer;
            TextExtractor = textExtractor;
        }

        public override async Task ProcessMessageAsync(NoteCreatedMessage message)
        {
            // Extract text
            var text = await TextExtractor.FromImage(message.Image);

            // Construct response message
            var responseMessage = new NoteProcessedMessage() { NoteId = message.NoteId, Content = text };

            // Send return message 
            await MessageProducer.SendAsync(responseMessage, "notes.processed");
        }
    }
}
