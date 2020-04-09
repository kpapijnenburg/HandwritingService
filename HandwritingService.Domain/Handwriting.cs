using System;

namespace HandwritingService.Domain
{
    public class Handwriting
    {
        public byte[] Image { get; set; }
        public State State { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
