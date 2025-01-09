using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.DAL.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    IQueryable<Category> GetAll();
    IQueryable<Category> Filter(CategorySC sc);
}