using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.DAL.Repository;

public class ServiceFileRepository : GenericRepository<ServiceFile>, IServiceFileRepository
{
    public ServiceFileRepository(SystemVaultDbContext dbContext) : base(dbContext)
    {
    }
}