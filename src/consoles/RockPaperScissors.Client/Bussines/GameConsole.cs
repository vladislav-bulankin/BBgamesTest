using RockPaperScissors.Client.Bussines.Abstractions;

namespace RockPaperScissors.Client.Bussines;



public class GameConsole : IGameConsole {

    private readonly IUserServiceClient userServiceClient; 
    private readonly IConsoleComander comander;

    private CurrentUser user;

    public GameConsole (
        IUserServiceClient serviceClient, 
        IMatchServiceClient matchServiceClient,
        IConsoleComander comander) {
        this.userServiceClient = serviceClient;
        this.comander = comander;
    }


    public async Task WorkAsync () {
        Console.WriteLine("Игра камень ножницы бумага");
        await SetUser();
        string comand = string.Empty;
        await Play();
        
    }

    public async Task Play () {
        string comand = string.Empty;
        while (!comand.Equals("out")) {
            comander.ShowCommands();
            Console.Write("Введите команду ");
            var command = Console.ReadLine();
            var task = comander.SetCommand(command ?? string.Empty);
            try {
                await task;
            } catch (Exception) {    }
        }
    }

    private async Task SetUser () {
        while (true) {
            Console.Write("Введите логин или \"out\" для выхода ");
            var login = Console.ReadLine();
            if (login?.Equals("out") ?? true) { Console.Out.Close(); }
            var comandResult = await userServiceClient.SetUserIdAsync(login ?? string.Empty);
            if (!comandResult.IsSuccess) {
                Console.WriteLine($"при попытке зарегистрировать пользователя произошла ошибка {comandResult.ErrorMessage}");
            } else {
                this.user = CurrentUser.GetInstance(login, comandResult.UserId);
                break;
            }
        }
        if (user is null) { Console.Out.Close(); }
    }
}
