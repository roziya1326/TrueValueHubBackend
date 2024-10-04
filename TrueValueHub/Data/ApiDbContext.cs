using Microsoft.EntityFrameworkCore;
using TrueValueHub.Models;

namespace TrueValueHub.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Project> Projects { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Part>()
                .HasMany(p => p.Materials)
                .WithOne(m => m.Part)
                .HasForeignKey(m => m.PartId);
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Parts)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectId);
            modelBuilder.Entity<Part>()
                .HasOne(p => p.ParentPart)            // Each Part can have one ParentPart
                .WithMany(p => p.ChildParts)          // Each ParentPart can have many ChildParts
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of parents when children exist

            


        }
    

    }
}
