using RockPaperScissors.Client.Bussines.Abstractions;
using RockPaperScissors.Client.Models;
using RockPaperScissors.Client.Protos;
using System.Text.RegularExpressions;

namespace RockPaperScissors.Client.Bussines;
public class ConsoleComander : IConsoleComander {

    private readonly IUserServiceClient userServiceClient;
    private readonly IMatchServiceClient matchServiceClient;
    private readonly INotificationServiceClient notificationServiceClient;

    public ConsoleComander (IUserServiceClient userServiceClient,
        IMatchServiceClient matchServiceClient,
        INotificationServiceClient notificationServiceClient) {
        this.userServiceClient = userServiceClient;
        this.matchServiceClient = matchServiceClient;
        this.notificationServiceClient = notificationServiceClient;
    }
    public Task SetCommand (string command) {
        if (TryParseJoinCommand(command, out var matchId)) {
            return JoinToMatch(matchId);
        } 

        if (TryParseCreateTransferCommand(command, out var userName, out var matchAmount)) {
            return CreateUserTransaction(userName, matchAmount);
        }

        if (TryParseCreateCommand(command, out var bitAmount)) {
            return CreateMatch(bitAmount);
        } else {
            return HandleSpecialCommands(command);
        }
    }

    public void ShowCommands () {
        Console.WriteLine("Список команд");
        Console.WriteLine("Список игр = games");
        Console.WriteLine("Показать список команд = help");
        Console.WriteLine("Присоедениться к игре = join game_id где game_id это идентификатор игры");
        Console.WriteLine("Создать матч create bit где bit это ставка матча");
        Console.WriteLine("Пперевести денги пользователю send_money amount to userName" +
            "\r\nгде amount это сумма целые и вещественные части разделены точкой" +
            "\r\nuserName имя пользователя");
        Console.WriteLine("Выйти из игры out");
    }

    private async Task CreateUserTransaction (string userName, double amount) {
        var toUser = await userServiceClient.GetUserAsync(userName);
        if (toUser is null) {
            await Reload($"Не удалось получить найти пользователя {userName}");
        } else {
            UserDto fromUser = new UserDto() {
                UserName = CurrentUser.GetInstance().GetName(),
                UserId = CurrentUser.GetInstance().GetId(),
            };
            var requstDto = await userServiceClient
                .CreateTransaction(toUser: toUser, fromUser: fromUser, amount: amount);
            if (requstDto is null || !requstDto.IsSuccess) {
                await Reload($"Не удалось совершить перевод ошибка {requstDto?.Error ?? string.Empty}");
            } else {
                await Reload("перевод выполнен успешно");
            }
        }
    }

    private async Task CreateMatch (double bit) {
        var move = CreateMove();
        var matchDto = await matchServiceClient.CreateMatchAsync(bit, move);
        var subscrebeObj = new NotificationRequst() {
            UserId = CurrentUser.GetInstance().GetId(),
            GameId = matchDto.MatchId,
            Move = move
        };
        Console.WriteLine($"Вы выбрали {move} ждем второго игрока");
        notificationServiceClient.SubscriptionCompleted += OnSubscriptionCompleted;
        await notificationServiceClient.SubscribeToNotifications(subscrebeObj);
    }

    private void OnSubscriptionCompleted (object? sender, EventArgs e) {
        notificationServiceClient.SubscriptionCompleted -= OnSubscriptionCompleted;
    }

    private async Task JoinToMatch (int matchId) {
        Console.WriteLine($"игра номер {matchId}");
        var move = CreateMove();
        var match = await userServiceClient
            .JoinMatch(matchId, move, CurrentUser.GetInstance());
        if (match is not null && match.IsSuccess) {
            var winName = match.WinnerName is null ? "ничья" :
                match.WinnerName.Equals(match.Player1Name) ? match.Player1Name : match.Player2Name;
            Console.WriteLine("Игра завершена");
            Console.WriteLine($"ваш ход {match.Player2Move} " +
                $"\r\n ход противника {match.Player1Move}" +
                $"\r\nпобедитель {winName}");
            Console.Write("нажмите любую клавишу что бы продолжить");
            Console.ReadKey();
        }
    }

    private string CreateMove () {
        string move = string.Empty;
        while (true) {
            Console.Write("Сделайте свой ход выберете \"R\" \"S\" или \"P\" ");
            move = Console.ReadLine();
            if (move.Equals("R") || move.Equals("S") || move.Equals("P")) {
                return move;
            } else {
                Console.WriteLine("Выбирете 1 из 3-х вариантов");
                continue;
            }
        }
    }

    private async Task Reload (string message = null) {
        if (!string.IsNullOrWhiteSpace(message)) {
            Console.WriteLine(message);
        }
        ShowCommands();
    }


    private bool TryParseJoinCommand (string command, out int matchId) {
        var match = Regex.Match(command, @"join (\d+)");
        return int.TryParse(match.Groups[1].Value, out matchId);
    }

    private bool TryParseCreateTransferCommand (string command, out string userName, out double matchAmount) {
        var match = Regex.Match(command, @"send_money ([\d]+(\.[\d]+)?) to (\w+)");
        userName = match.Groups[3].Value;
        return double.TryParse(match.Groups[1].Value, out matchAmount) && !string.IsNullOrEmpty(userName);
    }

    private bool TryParseCreateCommand (string command, out double bitAmount) {
        var match = Regex.Match(command, @"create (\d+(\.\d+)?)");
        return double.TryParse(match.Groups[1].Value, out bitAmount);
    }

    private Task HandleSpecialCommands (string command) {
        switch (command) {
            case "games":
                return GetMatchList();
            case "help":
                return Reload();
            case "out":
                return Task.Run(() => Environment.Exit(0));
            default:
                return Reload("Команда не определена");
        }
    }

    public async Task GetMatchList () {
        await foreach (var item in matchServiceClient.GatMatchListAsync()) {
            Console.WriteLine(item.ToString());
        }
    }
}
