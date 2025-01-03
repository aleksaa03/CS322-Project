using AutoMapper;
using SystemVault.BLL.DTOs;
using SystemVault.DAL.Models;

namespace SystemVault.BLL.AutoMapper;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}