using System;

namespace HandwritingService.Domain
{
    public class Handwriting : BaseEntity
    {
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public State State { get; set; }
        public Note Note { get; set; }
    }
}
