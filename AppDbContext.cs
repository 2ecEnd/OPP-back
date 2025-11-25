using OPP_back.Models;
using OPP_back.Models.Data;
using Data = OPP_back.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace OPP_back
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Data.Task> Tasks { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasMany(u => u.Subjects)
                    .WithOne(s => s.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Members)
                    .WithOne(m => m.User)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasMany(s => s.Tasks)
                    .WithOne(t => t.Subject)
                    .HasForeignKey(t => t.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Data.Task>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.HasMany(t => t.SubTasks)
                    .WithOne(t => t.SuperTask)
                    .HasForeignKey(t => t.SuperTaskId);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);
            });

            modelBuilder.Entity<AssignedTask>(entity =>
            {
                entity.HasKey(at => new { at.MemberId, at.TaskId });
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);

                entity.HasOne(rt => rt.User)
                    .WithMany()
                    .HasForeignKey(rt => rt.UserId);
            });
        }
    }
}