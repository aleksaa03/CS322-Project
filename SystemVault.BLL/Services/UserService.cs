﻿using AutoMapper;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.BLL.Services;

public class UserService : GenericService<User, UserDto, IUserRepository>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return _mapper.Map<UserDto?>(user);
    }
}