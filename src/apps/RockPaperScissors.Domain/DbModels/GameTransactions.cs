namespace RockPaperScissors.Domain.DbModels;
public class GameTransactions {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationType { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public Users? User { get; set; }
}

