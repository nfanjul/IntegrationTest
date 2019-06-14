using IntegrationTest.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.Api.Data
{
    public class AplicationDbContextSeed
    {
        private readonly AplicationDbContext _context;
        private readonly ILogger<AplicationDbContextSeed> _logger;
        // SHOW 6
        // SHOW 7 --
        public AplicationDbContextSeed(AplicationDbContext context, ILogger<AplicationDbContextSeed> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task EnsureSeedAsync()
        {
            if(!_context.Teams.Any())
            {
                _logger.LogInformation("Seeding teams...");
                _context.Teams.AddRange(GetTeams());
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Team> GetTeams()
        {
            return new List<Team>()
            {
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Real Sporting de Gijón",
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Real Madrid CF",
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "FC Barcelona",
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Valencia",
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Atletico de Madrid",
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Real Betis balompié",
                }
            };
        }

    }
}
