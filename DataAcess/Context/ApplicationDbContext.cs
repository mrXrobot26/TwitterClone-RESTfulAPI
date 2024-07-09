using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.MyModels.App;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options){ }

        public DbSet<UserProfile> profiles { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserProfile>()
                     .HasMany(p => p.Posts)
                     .WithOne(p => p.Profile)
                     .HasForeignKey(p => p.ProfileId);
        }
    }
    
}
