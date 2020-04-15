using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandwritingService.Domain;
using KPA.Database.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandwritingService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandwritingController : ControllerBase
    {
        private readonly IRepository<Handwriting> repository;

        public HandwritingController(IRepository<Handwriting> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAll());
        }
    }
}