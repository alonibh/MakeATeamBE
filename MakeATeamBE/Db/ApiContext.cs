using MakeATeamBE.Db.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MakeATeamBE.Db
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AppDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDbo>().Property(u => u.Teams)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<List<int>>(v));

            modelBuilder.Entity<TeamDbo>().Property(u => u.Players)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<(int, string)>>(v));
        }
        public DbSet<UserDbo> Users { get; set; }
        public DbSet<TeamDbo> Teams { get; set; }
        public DbSet<RatingDbo> Ratings { get; set; }

    }
}
