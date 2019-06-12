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
        public DbSet<Position> Positions { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetTeamModel(builder);
            SetPlayerModel(builder);
            SetPositionModel(builder);
        }

        private void SetTeamModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Team>().HasKey(x => x.Id);
        }

        private void SetPlayerModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Player>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>()
                .HasOne(s => s.Position);
        }

        private void SetPositionModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Position>().HasKey(x => x.Id);
            modelBuilder.Entity<Position>().HasData(PositionSeed().ToArray());
        }

        private static List<Position> PositionSeed()
        {
            return new List<Position>()
            {
                new Position()
                {
                    Id = Guid.NewGuid(),
                    Name = "Portero"
                },
                new Position()
                {
                    Id = Guid.NewGuid(),
                    Name = "Defensa"
                },
                new Position()
                {
                    Id = Guid.NewGuid(),
                    Name = "Centrocampista"
                },
                new Position()
                {
                    Id = Guid.NewGuid(),
                    Name = "Delantero"
                },
            };
        }

    }
}
