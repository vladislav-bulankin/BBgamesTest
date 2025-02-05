using RockPaperScissors.Domain.DbModels;

namespace RockPaperScissors.DataAccess.Abstractions;
public interface IUserDao {
    Task<int> AddUserAsync (Users user);
    Task<bool> CheckExistsAsync (string userName);
    Task CreateUserTransferAsync (GameTransactions transaction);
    Task<Users?> GetUserAsync (string userName);
    Task<Users?> GetUserAsync (int userId);
    Task<IEnumerable<Users>> GetUsersAsync (params int?[] ids);
    Task<double> GetIncomingPayis (int userId);
    Task<double> GetOutgoingPayis (int userId);
    Task<Users?> GetUserById (int userId);
}
