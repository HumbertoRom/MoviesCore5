using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> GetActor()
        {
            var actors = await _context.Actor.ToListAsync();
            return Ok(_mapper.Map<List<ActorDto>>(actors));
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
            var actor = _mapper.Map<Actor>(model);
            _context.Actor.Add(actor);
            await _context.SaveChangesAsync();
            var actorDto = _mapper.Map<ActorDto>(actor);
            return new CreatedAtRouteResult("GetActor", new { id = actor.ActorID }, actorDto);
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, [FromBody] CreateActorDto model)
        {
            var actor = _mapper.Map<Actor>(model);
            actor.ActorID = id;
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
