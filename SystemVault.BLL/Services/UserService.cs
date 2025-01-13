using AutoMapper;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

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

    public async Task<UserDto?> LoginUserAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null || user.Password != password) 
        {
            throw new Exception("User don't exist in the system.");
        }

        return _mapper.Map<UserDto?>(user);
    }

    public IQueryable<UserDto> Filter(UserSC sc)
    {
        var list = _userRepository.Filter(sc);
        return _mapper.ProjectTo<UserDto>(list);
    }
}