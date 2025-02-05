namespace RockPaperScissors.Domain.BussnesModels;
public class JoinToMatchResponseBM {
    public int GameId { get; set; }
    public string? Player1Name { get; set; }
    public string? Player2Name { get; set; }
    public string? Player1Move { get; set; }
    public string? Player2Move { get; set; }
    public int? Player1Id { get; set; }//это нужно в уведамлениях
    public int WinnerId { get; set; }
    public string? WinnerName { get; set; }
    public bool IsSuccess { get; set; }
}
