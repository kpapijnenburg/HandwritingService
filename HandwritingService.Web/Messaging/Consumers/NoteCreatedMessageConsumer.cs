using BIED.Messaging;
using BIED.Messaging.Abstractions;
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
        public IMessageProducer MessageProducer{ get; set; }

        public NoteCreatedMessageConsumer(IConnection connection, IMessageProducer messageProducer) : base(connection, "notes.created", "")
        {
            MessageProducer = messageProducer;
        }

        public override Task ProcessMessageAsync(NoteCreatedMessage message)
        {
            Console.WriteLine("Got message with NoteID" + message.NoteId);
            return Task.CompletedTask;
        }
    }
}
