using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.MyModels.App;
using Models.MyModels.PostFolder;
using Models.MyModels.ProfileModels;

namespace DataAcess.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                      .HasOne(p => p.ApplicationUser)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.ApplicationUserId);


            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.ApplicationUser)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
