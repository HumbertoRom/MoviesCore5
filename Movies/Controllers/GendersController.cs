using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GendersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GendersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDto>>> Get()
        {
            var genders = await _context.Genders.ToListAsync();
            return Ok(_mapper.Map<List<GenderDto>>(genders));
        }

        [HttpGet("{id:int}", Name = "GetGender")]
        public async Task<ActionResult<GenderDto>> Get(int id)
        {
            var gender = await _context.Genders.FirstOrDefaultAsync(x => x.GenderID == id);
            if (gender == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenderDto>(gender));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateGenderDto model)
        {
            var gender = _mapper.Map<Gender>(model);
            _context.Genders.Add(gender);
            await _context.SaveChangesAsync();
            var genderDto = _mapper.Map<GenderDto>(gender);

            return new CreatedAtRouteResult("GetGender", new { id = genderDto.GenderID }, genderDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateGenderDto model)
        {
            var gender = _mapper.Map<Gender>(model);
            gender.GenderID = id;
            _context.Entry(gender).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var gender = await _context.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            _context.Genders.Remove(gender);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
