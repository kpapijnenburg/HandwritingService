using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandwritingService.Web.Messaging.Messages
{
    public class NoteCreatedMessage
    {
        public int NoteId { get; set; }
        public byte[] Image { get; set; }

    }
}
