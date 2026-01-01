using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace ProfileService.Infrastructure.Persistence
{
    public class AppDbContext : DbContext

    {
        public DbSet<Profile> Profiles => Set<Profile>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(e =>
            {
                e.ToTable("profiles");
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Email).IsUnique();


            });

        }

    }
}
