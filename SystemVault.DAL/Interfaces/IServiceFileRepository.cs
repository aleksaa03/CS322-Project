using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.DAL.Interfaces;

public interface IServiceFileRepository : IGenericRepository<ServiceFile>
{
    IQueryable<ServiceFile> Filter(ServiceFileSC sc);
}