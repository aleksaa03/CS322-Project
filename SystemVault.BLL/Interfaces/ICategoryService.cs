using SystemVault.BLL.DTOs.Category;

namespace SystemVault.BLL.Interfaces;

public interface ICategoryService : IGenericService<CategoryDto>
{
    IQueryable<CategoryDto> GetAll();
}