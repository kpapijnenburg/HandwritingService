using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HandwritingService.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HandwritingService.Web.Controllers
{
    public class TestController : Controller
    {
        private IWebHostEnvironment _environment;

        public TestController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: /<controller>/
        [HttpGet("/test")]
        public async Task<IActionResult> Index()
        {
            var extractor = new GoogleCloudTextExtractor();
            
            var path = extractor.GetPath("credentials.json");

            var result = await extractor.FromImage(new byte[0]);

            return Ok(result);
        }
    }
}
