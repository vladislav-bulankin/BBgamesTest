using Grpc.Net.Client;
using RockPaperScissors.Client.Bussines.Abstractions;
using RockPaperScissors.Client.Protos;

namespace RockPaperScissors.Client.Bussines;
public class NotificationServiceClient : INotificationServiceClient {

    public event EventHandler SubscriptionCompleted;
    public async Task SubscribeToNotifications (NotificationRequst requst) {
        var channel = GrpcChannel.ForAddress("https://localhost:1002");
        var client = new Notification.NotificationClient(channel);
        using var call = client.Subscribe(requst);
        while (await call.ResponseStream.MoveNext(CancellationToken.None)) {
            var notifi = call.ResponseStream.Current;
            var message = $"Игра id {notifi.GameId} завершена игрок {notifi.Player1Name} выбрал {notifi.Player1Move}\r\n" +
                $" игрок {notifi.Player2Name} выбрал {notifi.Player2Move} победил {notifi.WinnerName}";
            Console.WriteLine(message);
            break;
        }
        SubscriptionCompleted?.Invoke(this, EventArgs.Empty);
    }
}
