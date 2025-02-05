using GetExchange_Rate.BankApiClient;
using GetExchange_Rate.BankApiClient.Abstractions;
using GetExchange_Rate.Bussines;
using GetExchange_Rate.Bussines.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GetExchange_Rate;


public class ServiceInstaller {

    private IHostApplicationBuilder builder;
    public ServiceInstaller (string[] args) {
        builder = Host.CreateApplicationBuilder(args);
        Install();
    }

    private void Install () {

        IHostEnvironment env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        AddSingletonServices();
       
        
        builder.Configuration.Build();
    }

    public T GetService<T> () {
        return builder.Services.BuildServiceProvider().GetRequiredService<T>();
    }


    private void AddSingletonServices () {
        builder.Services.AddSingleton<ICentralBankApiClient, CentralBankApiClient>();
        builder.Services.AddSingleton<ICurrencyService, CurrencyService>();
        builder.Services.AddSingleton<IConsole, Bussines.Console>();
    }
}
