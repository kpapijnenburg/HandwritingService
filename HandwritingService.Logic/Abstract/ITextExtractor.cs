using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingService.Logic.Abstract
{
    public interface ITextExtractor
    {
        Task<string> FromImage(byte[] imageData);
    }
}
