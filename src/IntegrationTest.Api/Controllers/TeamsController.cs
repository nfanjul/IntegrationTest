using IntegrationTest.Api.Data;
using IntegrationTest.Api.Models.Teams;
using IntegrationTest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IntegrationTest.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public TeamsController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Team>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var teams = await _context.Teams.ToListAsync();
            if(!teams.Any())
            {
                return NotFound($"Teams not found");
            }
            return Ok(teams);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Team), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateTeam request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("The name is empty.");
            }
            if (_context.Teams.Any(a => a.Name.Equals(request.Name)))
            {
                return BadRequest("Already exist a team with the same name.");
            }
            var team = new Team()
            {
                Name = request.Name,
                Id = Guid.NewGuid(),
            };
            _context.Teams.Add(team);
            var result = await _context.SaveChangesAsync();
            if(result <= 0)
            {
                return BadRequest("There was an error saving the Team.");
            }
            return Ok(team);
        }

    }
}