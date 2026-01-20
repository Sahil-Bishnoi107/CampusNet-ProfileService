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
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Report> Reports => Set<Report>();

        public DbSet<ProfileOtps> ProfileOtps => Set<ProfileOtps>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(e =>
            {
                e.ToTable("profiles");
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Email).IsUnique();


            });

            modelBuilder.Entity<Review>(e =>
            {
                e.ToTable("reviews");
                e.HasKey(x => x.id);
                e.HasIndex(x => new { x.reviewerId, x.reviewedId }).IsUnique();
            });

            modelBuilder.Entity<Report>(e =>
            {
                e.ToTable("reports");
                e.HasKey(x => x.id);
                e.HasIndex(x => new { x.reporterId, x.reportedId }).IsUnique();
            });

            modelBuilder.Entity<ProfileOtps>(e =>
            {
                e.ToTable("profile_otps");
                e.HasKey(x => x.Id);
                
            });

        }
        

    }
}
