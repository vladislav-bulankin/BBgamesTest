namespace RockPaperScissors.Client.Models;
public class MatchDto {
    public int MatchId { get; set; }
    public string? Player1Name { get; set; }
    public string? Player2Name { get; set; }
    public string? Player1Move { get; set; }
    public string? Player2Move { get; set; }
    public string? WinnerName { get; set; }
    public double Bet { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
}
