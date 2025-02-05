using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RockPaperScissors.Client.Bussines;
using RockPaperScissors.Client.Bussines.Abstractions;

namespace RockPaperScissors.Client;



public class ServiceInstaller {
    private readonly IHostApplicationBuilder builder;

    public ServiceInstaller (string[] args) {
        builder = Host.CreateApplicationBuilder(args);
        Install();
    }

    private void Install () {
        IHostEnvironment env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        
        AddSingletonServices();


        builder.Configuration.Build();
    }

    public T GetService<T> () {
        return builder.Services.BuildServiceProvider().GetRequiredService<T>();
    }

    private void AddSingletonServices () {
        builder.Services.AddSingleton<INotificationServiceClient, NotificationServiceClient>();
        builder.Services.AddSingleton<IConsoleComander,  ConsoleComander>();
        builder.Services.AddSingleton<ICurrentUser, CurrentUser>();
        builder.Services.AddSingleton<IMatchServiceClient, MatchServiceClient>();
        builder.Services.AddSingleton<IUserServiceClient, UserServiceClient>();
        builder.Services.AddSingleton<IGameConsole, GameConsole>();
    }
}
