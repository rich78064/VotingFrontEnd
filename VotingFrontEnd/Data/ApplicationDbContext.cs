using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingFrontEnd.Models;

namespace VotingFrontEnd.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>().HasData(
                 new Candidate { Id = 0003, Name = "Allen Iverson", PhotoUrl = "ai.jpg" },
                 new Candidate { Id = 0006, Name = "Bill Russell", PhotoUrl = "billrussell.jpg" },
                 new Candidate { Id = 0021, Name = "Tim Duncan", PhotoUrl = "timduncan.jpg" },
                 new Candidate { Id = 0030, Name = "Stephen Curry", PhotoUrl = "curry.jpg" },
                 new Candidate { Id = 0034, Name = "Hakeem Olajuwon", PhotoUrl = "thedream.jpg" },
                 new Candidate { Id = 0041, Name = "Dirk Nowitzki", PhotoUrl = "dirk.jpg" },
                 new Candidate { Id = 0824, Name = "Kobe Bryant", PhotoUrl = "kobe.jpg" },
                 new Candidate { Id = 2306, Name = "LeBron James", PhotoUrl = "lebron.jpg" },
                 new Candidate { Id = 2345, Name = "Michael Jordan", PhotoUrl = "mj.jpg" },
                 new Candidate { Id = 3201, Name = "Magic Johnson", PhotoUrl = "magic.jpg" },
                 new Candidate { Id = 3202, Name = "Karl Malone", PhotoUrl = "mailman.jpg" },
                 new Candidate { Id = 3301, Name = "Kareem Abdul-Jabbar", PhotoUrl = "kareem.jpg" },
                 new Candidate { Id = 3302, Name = "Larry Bird", PhotoUrl = "bird.jpg" },
                 new Candidate { Id = 3432, Name = "Shaquille O'Neal", PhotoUrl = "shaq.jpg" }
                );
        }

    }
}
