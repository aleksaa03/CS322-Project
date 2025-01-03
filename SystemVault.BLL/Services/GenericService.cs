using AutoMapper;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.BLL.Services;

public class GenericService<TDomainModel, TDtoModel, TRepository> : IGenericService<TDtoModel> 
                                                                    where TDomainModel : Base 
                                                                    where TRepository : IGenericRepository<TDomainModel>
{
    private readonly TRepository _repository;
    private readonly IMapper _mapper;

    public GenericService(TRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDtoModel?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            return default;
        }

        return _mapper.Map<TDtoModel?>(entity);
    }

    public async Task CreateAsync(TDtoModel entity)
    {
        var domainEntity = _mapper.Map<TDomainModel>(entity);
        await _repository.CreateAsync(domainEntity);
    }

    public void Update(TDtoModel entity)
    {
        var domainEntity = _mapper.Map<TDomainModel>(entity);
        _repository.Update(domainEntity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> EntityExistAsync(long id)
    {
        return await _repository.EntityExistAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _repository.SaveChangesAsync();
    }
}