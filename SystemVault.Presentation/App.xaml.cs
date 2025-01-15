using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using System.Windows;
using SystemVault.BLL;
using SystemVault.DAL.Context;
using SystemVault.Presentation.Helpers;
using SystemVault.Presentation.Views;
using SystemVault.Presentation.Views.UserControls;
using SystemVault.Presentation.Views.Windows.Category;
using SystemVault.Presentation.Views.Windows.ServiceFile;
using SystemVault.Presentation.Views.Windows.User;

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
        RegisterExceptionHandler();

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
        serviceCollection.AddTransient(typeof(LoginView));

        serviceCollection.AddTransient(typeof(FileView));
        serviceCollection.AddTransient(typeof(AddFileWindow));
        serviceCollection.AddTransient(typeof(EditFileWindow));

        serviceCollection.AddTransient(typeof(CategoryView));
        serviceCollection.AddTransient(typeof(AddCategoryWindow));

        serviceCollection.AddTransient(typeof(UserView));
        serviceCollection.AddTransient(typeof(AddUserWindow));
        serviceCollection.AddTransient(typeof(EditUserWindow));
        serviceCollection.AddTransient(typeof(ResetPasswordWindow));
    }

    private void RegisterExceptionHandler()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            HandleException((Exception)e.ExceptionObject);
        };

        DispatcherUnhandledException += (sender, e) =>
        {
            e.Handled = true;
            HandleException(e.Exception);
        };

        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            HandleException(e.Exception);
        };
    }

    private void HandleException(Exception exception)
    {
        MessageBoxHelper.ShowError(exception.Message);
    }
}