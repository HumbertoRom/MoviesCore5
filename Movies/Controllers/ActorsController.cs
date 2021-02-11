using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.Services;

namespace Movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageFiles _storageFiles;
        private readonly string container = "Actors";
        public ActorsController(ApplicationDbContext context, IMapper mapper, IStorageFiles storageFiles)
        {
            _context = context;
            _mapper = mapper;
            _storageFiles = storageFiles;

        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> GetActor()
        {
            return Ok(_mapper.Map<List<ActorDto>>(await _context.Actor.ToListAsync()));
        }

        // GET: api/Actors/5
        [HttpGet("{id}", Name = "GetActor")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ActorDto>(actor));
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostActor([FromForm] CreateActorDto model)
        {
            Actor actor = _mapper.Map<Actor>(model);
            if (model.Photo != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await model.Photo.CopyToAsync(memoryStream);
                    byte[] content = memoryStream.ToArray();
                    string extension = Path.GetExtension(model.Photo.FileName);
                    actor.Photo = await _storageFiles.SaveFile(content, extension, container, model.Photo.ContentType);
                }
            }
            _context.Actor.Add(actor);
            await _context.SaveChangesAsync();
            var actorDto = _mapper.Map<ActorDto>(actor);

            return new CreatedAtRouteResult("GetActor", new { id = actor.ActorID }, actorDto);
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutActor(int id, [FromForm] CreateActorDto model)
        {
            Actor actor = _mapper.Map<Actor>(model);
            actor.ActorID = id;
            if (model.Photo != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await model.Photo.CopyToAsync(memoryStream);
                    byte[] content = memoryStream.ToArray();
                    string extension = Path.GetExtension(model.Photo.FileName);
                    actor.Photo = await _storageFiles.EditFile(content, extension, container, model.Photo.ContentType, actor.Photo);
                }
            }
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            if (actor.Photo != null)
            {
                await _storageFiles.DeleteFile(actor.Photo, container);
            }
            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
