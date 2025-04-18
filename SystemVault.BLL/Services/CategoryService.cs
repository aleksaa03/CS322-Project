﻿using AutoMapper;
using SystemVault.BLL.DTOs.Category;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.BLL.Services;

public class CategoryService : GenericService<Category, CategoryDto, ICategoryRepository>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public IQueryable<CategoryDto> GetAll()
    {
        var list = _categoryRepository.GetAll();
        return _mapper.ProjectTo<CategoryDto>(list);
    }

    public IQueryable<CategoryDto> Filter(CategorySC sc)
    {
        var list = _categoryRepository.Filter(sc);
        return _mapper.ProjectTo<CategoryDto>(list);
    }
}