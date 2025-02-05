using RockPaperScissors.Domain.BussnesModels;


namespace RockPaperScissors.Bussines.Abstractions;


public interface IUserService {
    Task<bool> CheckExistsAsync (string userName);
    Task<UserMoneyTransferResponse> CreateMoneyTransferAsync (int toUserId, int fromUserId, double amount);
    Task<Gamer?> CreateUserAsync (string userName);
    Task<Gamer> GetUserAsync (string userName);
    Task<double> GetUserBalanceAsync (int userId);
}
