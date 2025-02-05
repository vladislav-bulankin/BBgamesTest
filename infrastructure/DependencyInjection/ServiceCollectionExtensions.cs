using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Bussines.Abstractions;
using RockPaperScissors.Bussines.Match;
using RockPaperScissors.Bussines.User;
using RockPaperScissors.DataAccess.Abstractions;
using RockPaperScissors.DataAccess.MatchDao;
using RockPaperScissors.DataAccess.UserDao;


namespace infrastructure.DependencyInjection;


public static class ServiceCollectionExtensions {
    public static void InjectAsScope(this IServiceCollection services) {
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IMatchDao, MatchDao>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IUserService, UserService>();
    }
}
