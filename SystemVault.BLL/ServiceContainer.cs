using Microsoft.Extensions.DependencyInjection;
using SystemVault.BLL.AutoMapper;
using SystemVault.BLL.Interfaces;
using SystemVault.BLL.Services;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Repository;

namespace SystemVault.BLL;

public static class ServiceContainer
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        serviceCollection.AddScoped<IUserService, UserService>();

        serviceCollection.AddAutoMapper(typeof(MapProfiles));
    }
}