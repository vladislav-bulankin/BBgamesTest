using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using RockPaperScissors.Client.Bussines.Abstractions;
using RockPaperScissors.Client.Models;
using RockPaperScissors.Client.Protos;

namespace RockPaperScissors.Client.Bussines;


public class MatchServiceClient : IMatchServiceClient {

    private string serviceIp;
    const string defaultIp = "https://localhost:1002";

    public MatchServiceClient (IConfiguration configuration) {
        serviceIp = configuration?.GetSection("Service").GetSection("deffaultIp")?.Value ?? defaultIp;
    }

    public async IAsyncEnumerable<MatchShortView> GatMatchListAsync () {
        var user = CurrentUser.GetInstance();
        if (user is null) { throw new NullReferenceException(nameof(CurrentUser)); }
        var request = new GetMatchListRequst() { };
        var client = await CreateClientAsync();
        using var response = client.GetMatchList(request);
        var responseStream = response.ResponseStream;
        Console.WriteLine("Список игр:");
        await foreach (var match in responseStream.ReadAllAsync()) {
            if (match is null) { yield break; }
            if (match?.PlayerName?.Equals(user.GetName()) ?? true) { continue; }
            yield return new() {
                GameId = match.GameId,
                Rate = match.GameRate,
                UserName = match.PlayerName ?? string.Empty
            };
        }
    }

    public async Task<MatchDto> CreateMatchAsync (double bet, string userMove) {
        var user = CurrentUser.GetInstance();
        var requst = new CreateMatchRequest() {
            UserName = user.GetName(),
            MatchRate = bet,
            PlayerChoice = userMove
        };
        var client = await CreateClientAsync();
        var response = client.CreateMatch(requst);
        if (response is null
            || response.GameId == 0) {
            return null;
        } else {
            return new() {
                IsSuccess = true,
                Player1Name = response.PlayerName
            };
        }
    }

    private async Task<Match.MatchClient> CreateClientAsync () {
        var channel = GrpcChannel.ForAddress(serviceIp);
        return new Match.MatchClient(channel);
    }
}
