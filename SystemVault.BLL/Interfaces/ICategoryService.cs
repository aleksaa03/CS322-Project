using SystemVault.BLL.DTOs.Category;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.BLL.Interfaces;

public interface ICategoryService : IGenericService<CategoryDto>
{
    IQueryable<CategoryDto> GetAll();
    IQueryable<CategoryDto> Filter(CategorySC sc);
}