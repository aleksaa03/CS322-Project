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
        serviceCollection.AddScoped<IServiceFileRepository, ServiceFileRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();

        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IServiceFileService, ServiceFileService>();
        serviceCollection.AddScoped<ICategoryService, CategoryService>();

        serviceCollection.AddAutoMapper(typeof(MapProfiles));
    }
}