using RockPaperScissors.Client.Models;

namespace RockPaperScissors.Client.Bussines.Abstractions;
public interface IUserServiceClient {

    Task<SetUserIdCommandResult> SetUserIdAsync (string commandName);
    Task<UserDto> GetUserAsync (string userName);
    Task<TransactionDto?> CreateTransaction (UserDto toUser, UserDto fromUser, double amount);
    Task<MatchDto> JoinMatch (int matchId, string move, CurrentUser user);
}
