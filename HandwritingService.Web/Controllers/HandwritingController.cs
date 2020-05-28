using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIED.Messaging.Abstractions;
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


        [HttpGet("/api/handwriting/notes/{noteId}")]
        public async Task<IActionResult> GetByNoteId(int noteId)
        {
            return Ok(await repository.FindAll(h => h.NoteId == noteId));
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

        [HttpPost("/api/handwriting")]
        public async Task<IActionResult> Create(Handwriting handwriting)
        {
            var result = await repository.Create(handwriting);

            return Created($"/api/handwriting/{result.Id}", result);
        }

        [HttpPut("/api/handwriting/{id}")]
        public async Task<IActionResult> Update(int id, Handwriting handwriting)
        {
            var inDb = await repository.GetById(id) != null;

            if(!inDb)
            {
                return NotFound();
            }

            await repository.Update(handwriting);

            return NoContent();
        }

        [HttpDelete("/api/handwriting/{id}")]
        public async Task<IActionResult> Delete(int id, Handwriting handwriting)
        {
            var inDb = await repository.GetById(id) != null;

            if (!inDb) return NotFound();

            await repository.Delete(handwriting);

            return NoContent();
        }
    }
}