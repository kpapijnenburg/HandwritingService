using Google.Cloud.Vision.V1;
using HandwritingService.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingService.Logic
{
    public class GoogleCloudTextExtractor : ITextExtractor
    {
        public async Task<string> FromImage(byte[] imageData)
        {
            ImageAnnotatorClient client = await GetClientAsync("credentials.json");

            var image = Image.FromBytes(imageData);

            var response = await client.DetectDocumentTextAsync(image);
            return response.Text;
        }

        private Task<ImageAnnotatorClient> GetClientAsync(string credentialsFile)
        {
            return new ImageAnnotatorClientBuilder()
            {
                CredentialsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), credentialsFile)
            }
            .BuildAsync();
        }
    }
}
