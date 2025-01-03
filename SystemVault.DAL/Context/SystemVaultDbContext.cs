using Microsoft.EntityFrameworkCore;
using SystemVault.DAL.Models;
using ServiceFile = SystemVault.DAL.Models.ServiceFile;

namespace SystemVault.DAL.Context;

public class SystemVaultDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ServiceFile> Files { get; set; }
    public DbSet<Category> Categories { get; set; }

    public SystemVaultDbContext(DbContextOptions<SystemVaultDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceFile>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.CategoryId);

        base.OnModelCreating(modelBuilder);
    }
}