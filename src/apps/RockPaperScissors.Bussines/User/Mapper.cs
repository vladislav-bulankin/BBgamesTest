using RockPaperScissors.Domain.DbModels;
using RockPaperScissors.Domain.BussnesModels;

namespace RockPaperScissors.Bussines.User;
internal static class Mapper {
    internal static GameTransactions CreateTransfer (int userId, double amount, OperationsType operandType) {
        return new() {
            UserId = userId,
            OperationType = (int)operandType,
            Amount = (decimal)amount,
            CreatedAt  = DateTime.UtcNow.ToUniversalTime(),
        };
    }
}
