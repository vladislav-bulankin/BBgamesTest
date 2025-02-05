using Grpc.Core;
using RockPaperScissors.Service.Protos;

namespace RockPaperScissors.Service.NotificationCollection;
public static class NotificationQueue {

    public static Dictionary<int, List<IServerStreamWriter<NotificationResponse>>> queue = new();

    public static void Add(int key, IServerStreamWriter<NotificationResponse> responseStream) {
        if (!queue.ContainsKey(key)) {
            queue[key] = new List<IServerStreamWriter<NotificationResponse>>();
        }
        queue[key].Add(responseStream);
    }

    public static List<IServerStreamWriter<NotificationResponse>> Get (int key) {
        if (queue.TryGetValue(key, out var result)) {
            queue.Remove(key);
            return result;
        } else {
            return null;
        }
    }
}
