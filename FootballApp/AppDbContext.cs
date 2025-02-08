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
        public DbSet<UserTeam> UserTeams { get; set; }

        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Match>().HasKey(m => m.Id);
            modelBuilder.Entity<UserTeam>().HasKey(ut => new { ut.UserId, ut.TeamId });


            modelBuilder.Entity<Team>()
       .HasOne(t => t.Captain)
       .WithMany()
       .HasForeignKey(t => t.CaptainId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithMany()
                .UsingEntity(j => j.ToTable("TeamMembers"));

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(ut => ut.TeamId);
        }
    }

}
