using SystemVault.DAL.Models;

namespace SystemVault.DAL.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    IQueryable<Category> GetAll();
}