using FootballApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FootballApp
{
    using Microsoft.EntityFrameworkCore;
    using FootballApp.Entities;

    public class FootballDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Match>().HasKey(m => m.Id);
        }
    }

}
