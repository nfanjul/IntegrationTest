using IntegrationTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IntegrationTest.Api.Data
{
    public class AplicationDbContext :  DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetTeamModel(builder);
            SetPlayerModel(builder);
        }

        private void SetTeamModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Team>().HasKey(x => x.Id);
            modelBuilder.Entity<Team>().HasData(TeamSeed().ToArray());
        }

        private static List<Team> TeamSeed()
        {
            // SHOW 3
            return new List<Team>()
            {
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Real Sporting de Gijón"
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "Real Betis"
                },
                new Team()
                {
                    Id = Guid.NewGuid(),
                    Name = "UP Langreo"
                }
            };
        }

        private void SetPlayerModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Player>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasData(PlayerSeed().ToArray());
        }

        private static List<Player> PlayerSeed()
        {
            // SHOW 3
            return new List<Player>()
            {
                new Player()
                {
                    Id = Guid.NewGuid(),
                    Name = "Diego Mariño"
                },
                new Player()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manu García"
                }
            };
        }

    }
}
