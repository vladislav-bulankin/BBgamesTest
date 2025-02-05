namespace RockPaperScissors.Domain.DbModels;
public class MatchHistory {
    public int Id { get; set; }
    public int? Player1Id { get; set; }
    public int? Player2Id { get; set; }
    public int? WinnerId { get; set; }
    public decimal Bet { get; set; }
    public string? MovePlayer1 { get; set; }
    public string? MovePlayer2 { get; set; }
    public DateTime CreatedAt { get; set; }

    public Users? Player1 { get; set; }
    public Users? Player2 { get; set; }
    public Users? Winner { get; set; }
}
