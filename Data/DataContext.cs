using Microsoft.EntityFrameworkCore;
using Web_API.Models;

namespace Web_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // For data seeding into the database
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill{Id = 1, Name = "FireBall", Damage = 30},
                new Skill{Id = 2, Name = "Lightning", Damage = 50},
                new Skill{Id = 3, Name = "Magic", Damage = 100}

            );
        }

        public DbSet<Character> Characters => Set<Character>();
        //Creates a table named characters in the database with EF Core code first

        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();



    }
}