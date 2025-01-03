using Microsoft.EntityFrameworkCore;
using SystemVault.DAL.Models;

namespace SystemVault.DAL.Context;

public class SystemVaultDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public SystemVaultDbContext(DbContextOptions<SystemVaultDbContext> options) : base(options)
    {
    }
}