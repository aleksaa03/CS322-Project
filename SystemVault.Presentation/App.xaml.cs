using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using System.Windows;
using SystemVault.BLL;
using SystemVault.DAL.Context;

namespace SystemVault.Presentation;

public partial class App : Application
{
    public IServiceProvider? ServiceProvider { get; set; }
    public IConfiguration? Configuration { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<SystemVaultDbContext>(options =>
        {
            options.EnableServiceProviderCaching();
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging(false);

            string? connectionString = Configuration?.GetConnectionString("DbConnectionString");

            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options =>
            {
                options.EnableRetryOnFailure();
                options.CommandTimeout(30);
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        });

        ServiceContainer.RegisterServices(serviceCollection);

        serviceCollection.AddTransient(typeof(MainWindow));
    }
}