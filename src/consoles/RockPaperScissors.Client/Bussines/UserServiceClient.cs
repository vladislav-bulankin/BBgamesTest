using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using RockPaperScissors.Client.Bussines.Abstractions;
using RockPaperScissors.Client.Models;
using RockPaperScissors.Client.Protos;

namespace RockPaperScissors.Client.Bussines;
public class UserServiceClient : IUserServiceClient {

    private string serviceIp;
    const string defaultIp = "https://localhost:1002";
    public UserServiceClient (IConfiguration configuration) {
        serviceIp = configuration?.GetSection("Service").GetSection("deffaultIp")?.Value ?? defaultIp;
    }

    public async Task<SetUserIdCommandResult> SetUserIdAsync (string userName) {
        var client = await CreateClientAsync();
        var response = client.IdentifyUser(new IdentityRequest { UserName = userName });

        if (response == null) {
            return new SetUserIdCommandResult { IsSuccess = false, ErrorMessage = "Server not responding." };
        }
        
        return response.IsSuccess
            ? new SetUserIdCommandResult { IsSuccess = true, UserId = response.UserId }
            : new SetUserIdCommandResult { IsSuccess = false, ErrorMessage = response.Error };
    }

    public async Task<UserDto?> GetUserAsync (string userName) {
        var client = await CreateClientAsync();
        var response = client.IdentifyUser(new IdentityRequest { UserName = userName });

        if (response != null && response.IsSuccess) {
            return new UserDto { UserId = response.UserId, UserName = userName };
        }

        return null;
    }

    public async Task<TransactionDto?> CreateTransaction (UserDto toUser, UserDto fromUser, double amount) {
        var request = new MoneyTransferRequest {
            Amount = amount,
            FromUserId = fromUser.UserId,
            ToUserId = toUser.UserId
        };

        var client = await CreateClientAsync();
        var response = await client.CreateMoneyTransferAsync(request);

        if (response == null || !response.IsSuccess) {
            return null;
        }

        return new TransactionDto { IsSuccess = true };
    }

    public async Task<MatchDto> JoinMatch (int matchId, string move, CurrentUser user) {
        var request = new JoinToMatchRequst {
            GameId = matchId,
            UserName = user.GetName(),
            Move = move,
        };

        var client = await CreateClientAsync();
        var response = await client.JoinToMatchAsync(request);

        if (response == null || !response.IsSuccess) {
            return new MatchDto { IsSuccess = false, Error = "Не удалось присоеденитца к матчу" };
        }

        return new MatchDto {
            IsSuccess = true,
            MatchId = response.GameId,
            Player1Name = response.Player1Name,
            Player2Name = response.Player2Name,
            Player1Move = response.Player1Move,
            Player2Move = response.Player2Move,
            WinnerName = response.WinnerName
        };
    }

    private async Task<User.UserClient> CreateClientAsync () {
        var channel = GrpcChannel.ForAddress(serviceIp);
        return new User.UserClient(channel);
    }
}