using AutoMapper;
using SystemVault.BLL.DTOs.Category;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.BLL.Services;

public class CategoryService : GenericService<Category, CategoryDto, ICategoryRepository>, ICategoryService
{
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
    {
    }
}