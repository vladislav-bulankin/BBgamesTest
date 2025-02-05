namespace RockPaperScissors.Domain.BussnesModels;
public class CreateBetResponseBM {
    public int GameId { get; set; }
    public int Player1Id { get; set; }
    public string? Player1Move { get; set; }
    public int? Player2Id { get; set; }
    public string? Player2Move { get; set; }
    public int? WinnerId { get; set; }
    public string? WinnerName { get; set; }
}
