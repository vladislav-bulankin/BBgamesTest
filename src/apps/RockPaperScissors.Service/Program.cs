using infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RockPaperScissors.DataAccess;
using RockPaperScissors.Service.Services;
using System.Configuration;


namespace RockPaperScissors.Service;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder
            .Configuration
            .SetBasePath(
                Directory
                .GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", true, true);
        // Add services to the container.
        builder.Services.AddGrpc();
        builder.Services.InjectAsScope();

        var app = builder.Build();

        app.UseStaticFiles();
        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.MapGrpcService<UserService>();
        app.MapGrpcService<MatchSevice>();
        app.MapGrpcService<NotificationService>();
        app.Run();
    }
}