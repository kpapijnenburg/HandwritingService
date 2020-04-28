using BIED.Messaging;
using BIED.Messaging.Abstractions;
using HandwritingService.Domain;
using HandwritingService.Logic.Abstract;
using HandwritingService.Web.Messaging.Messages;
using KPA.Database.Abstractions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandwritingService.Web.Messaging.Consumers
{
    public class NoteCreatedMessageConsumer : MessageConsumer<NoteCreatedMessage>
    {
        private readonly IMessageProducer MessageProducer;
        private readonly ITextExtractor TextExtractor;
        private readonly IRepository<Handwriting> Repository;

        public NoteCreatedMessageConsumer(IConnection connection, 
            IMessageProducer messageProducer, 
            ITextExtractor textExtractor, 
            IRepository<Handwriting> repository) : base(connection, "notes.created", "")
        {
            MessageProducer = messageProducer;
            TextExtractor = textExtractor;
            Repository = repository;
        }

        public override async Task ProcessMessageAsync(NoteCreatedMessage message)
        {
            var responseMessage = new NoteProcessedMessage() { NoteId = message.NoteId };

            try
            {
                var content = await TextExtractor.FromImage(message.Image);

                var handwriting = await CreateHandwriting(content, message.Image);

                responseMessage.state = State.Finished;
            }
            catch (Exception e)
            {
                responseMessage.Exception = e;
                responseMessage.state = State.Error;
            }
            finally
            {
                await MessageProducer.SendAsync(responseMessage, "notes.processed");
            }
        }

        private async Task<Handwriting> CreateHandwriting(string content, byte[] image)
        {
            return await Repository.Create(new Handwriting()
            {
                Content = content,
                Image = image,
                State = State.Finished,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }
    }
}
