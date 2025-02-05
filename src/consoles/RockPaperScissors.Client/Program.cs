using RockPaperScissors.Client.Bussines.Abstractions;

namespace RockPaperScissors.Client;
internal class Program {

    private static async Task Main (string[] args) {
        IGameConsole console;
        var serviceProvider = new ServiceInstaller(args);
        console = serviceProvider.GetService<IGameConsole>();
        await console.WorkAsync().ConfigureAwait(false);
    }

}
