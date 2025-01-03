using AutoMapper;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.BLL.Services;

public class ServiceFileService : GenericService<ServiceFile, ServiceFileDto, IServiceFileRepository>, IServiceFileService
{
    public ServiceFileService(IServiceFileRepository serviceFileRepository, IMapper mapper) : base(serviceFileRepository, mapper)
    {
    }
}