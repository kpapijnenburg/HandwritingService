using System;
using System.Collections.Generic;
using System.Text;

namespace HandwritingService.Domain
{
    public class Note
    {
        public int Id { get; set; }
        public Handwriting Handwriting { get; set; }
        public int HandwritingId { get; set; }
    }
}
