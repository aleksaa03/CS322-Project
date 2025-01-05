using AutoMapper;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.BLL.Services;

public class ServiceFileService : GenericService<ServiceFile, ServiceFileDto, IServiceFileRepository>, IServiceFileService
{
    private readonly IServiceFileRepository _serviceFileRepository;
    private readonly IMapper _mapper;

    public ServiceFileService(IServiceFileRepository serviceFileRepository, IMapper mapper) : base(serviceFileRepository, mapper)
    {
        _serviceFileRepository = serviceFileRepository;
        _mapper = mapper;
    }

    public IQueryable<ServiceFileDto> Filter(ServiceFileSC sc)
    {
        var list = _serviceFileRepository.Filter(sc);
        return _mapper.ProjectTo<ServiceFileDto>(list);
    }
}