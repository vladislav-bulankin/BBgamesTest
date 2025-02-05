using Grpc.Core;
using RockPaperScissors.Domain.BussnesModels;
using RockPaperScissors.Service.NotificationCollection;
using RockPaperScissors.Service.Protos;

namespace RockPaperScissors.Service.Services;
public class NotificationService : Notification.NotificationBase {

    public override async Task Subscribe
        (NotificationRequst request, 
        IServerStreamWriter<NotificationResponse> responseStream, 
        ServerCallContext context) {

        try {
            NotificationQueue.Add(request.UserId, responseStream);
        } catch (Exception ex) {
            Console.WriteLine($"Ошибка при добавлении в очередь: {ex.Message}");
            throw;
        }
        try {
            while (!context.CancellationToken.IsCancellationRequested) {
                await Task.Delay(3000);
            }
        } catch (Exception) { }
    }

    public async Task SendNotification (JoinToMatchResponseBM responseBM) {
        var responseStreams = NotificationQueue.Get(responseBM.Player1Id ?? 0);
        if (responseStreams is not null) {
            NotificationResponse response = new() {
                GameId = responseBM.GameId,
                Player1Name = responseBM.Player1Name,
                Player1Move = responseBM.Player1Move,
                Player2Name = responseBM.Player2Name,
                Player2Move = responseBM.Player2Move,
                WinnerName = responseBM.WinnerName
            };
            foreach (var responseStream in responseStreams) {
                await responseStream.WriteAsync(response);
            }
        }
    }
}
