using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.BLL.Interfaces;

public interface IServiceFileService : IGenericService<ServiceFileDto>
{
    IQueryable<ServiceFileDto> Filter(ServiceFileSC sc);
}