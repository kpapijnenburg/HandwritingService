using HandwritingService.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingService.Logic
{
    public class DummyTextExtractor : ITextExtractor
    {
        public Task<string> FromImage(byte[] imageData)
        {
            return Task.FromResult("Dummy \n Text \n Extractor");
        }
    }
}
