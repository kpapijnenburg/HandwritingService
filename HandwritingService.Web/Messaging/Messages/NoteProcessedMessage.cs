using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandwritingService.Web.Messaging.Messages
{
    public class NoteProcessedMessage
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
    }
}
