using MakeATeamBE.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeATeamBE.Db
{
    public class MakeATeamContext : DbContext
    {
        public MakeATeamContext(DbContextOptions<MakeATeamContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamDbo>().Property(u => u.Id).ValueGeneratedOnAdd();
        }

        public DbSet<UserDbo> Users { get; set; }
        public DbSet<UserTeamsDbo> UserTeams { get; set; }
        public DbSet<TeamDbo> Teams { get; set; }
        public DbSet<RatingDbo> Ratings { get; set; }

    }
}
