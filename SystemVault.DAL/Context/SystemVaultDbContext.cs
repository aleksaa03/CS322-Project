using Microsoft.EntityFrameworkCore;
using SystemVault.DAL.Models;
using File = SystemVault.DAL.Models.File;

namespace SystemVault.DAL.Context;

public class SystemVaultDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Category> Categories { get; set; }

    public SystemVaultDbContext(DbContextOptions<SystemVaultDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.CategoryId);

        base.OnModelCreating(modelBuilder);
    }
}