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
    [ApiController]
    public class HandwritingController : ControllerBase
    {
        private readonly IRepository<Handwriting> repository;

        public HandwritingController(IRepository<Handwriting> repository)
        {
            this.repository = repository;
        }

        [HttpGet("/api/handwriting")]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAll());
        }

        [HttpGet("/api/handwriting/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var handwriting = await repository.GetById(id);

            if (handwriting == null)
            {
                return NotFound();
            }

            return Ok(handwriting);
        }
    }
}