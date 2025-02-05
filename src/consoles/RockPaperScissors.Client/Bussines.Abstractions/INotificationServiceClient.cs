using RockPaperScissors.Client.Protos;
using System.Threading;

namespace RockPaperScissors.Client.Bussines.Abstractions;
public interface INotificationServiceClient {
    Task SubscribeToNotifications (NotificationRequst requst);
    event EventHandler SubscriptionCompleted;
}
