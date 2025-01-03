using AutoMapper;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.DTOs.Category;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.DAL.Models;

namespace SystemVault.BLL.AutoMapper;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<ServiceFile, ServiceFileDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}