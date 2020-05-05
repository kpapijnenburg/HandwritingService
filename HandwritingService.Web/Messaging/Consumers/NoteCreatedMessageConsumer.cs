using BIED.Messaging;
using BIED.Messaging.Abstractions;
using HandwritingService.Domain;
using HandwritingService.Logic.Abstract;
using HandwritingService.Web.Messaging.Messages;
using KPA.Database.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandwritingService.Web.Messaging.Consumers
{
    public class NoteCreatedMessageConsumer : MessageConsumer<NoteCreatedMessage>
    {
        private readonly ITextExtractor TextExtractor;
        private readonly IRepository<Handwriting> Repository;

        public NoteCreatedMessageConsumer(IConnection connection,
            ITextExtractor textExtractor,
            IRepository<Handwriting> repository) : base(connection, "notes.created", "")
        {
            TextExtractor = textExtractor;
            Repository = repository;
        }

        public override async Task ProcessMessageAsync(NoteCreatedMessage message)
        {
            if(message == null || message.Image == null || !message.Image.Any())
            {
                return;
            }

            var content = await TextExtractor.FromImage(message.Image);

            await CreateHandwriting(content, message.Image, message.NoteId);
        }

        private async Task<Handwriting> CreateHandwriting(string content, byte[] image, int noteId)
        {
            return await Repository.Create(new Handwriting()
            {
                Content = content,
                Image = image,
                State = State.Finished,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                NoteId = noteId
            });
        }
    }
}
