using System;
using System.Collections.Generic;
using System.Text;

namespace HandwritingService.Domain
{
    class BaseEntity
    {
        int Id;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
