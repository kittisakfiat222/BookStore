using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BookStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserLikes> UserLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Username ต้องไม่ซ้ำ
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // ป้องกันการกด like ซ้ำของ user + book
            modelBuilder.Entity<UserLikes>()
                .HasIndex(u => new { u.UserId, u.BookId })
                .IsUnique();
            // Relationship 
            modelBuilder.Entity<UserLikes>()
                 .HasOne(ul => ul.User)
                 .WithMany()
                 .HasForeignKey(ul => ul.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
