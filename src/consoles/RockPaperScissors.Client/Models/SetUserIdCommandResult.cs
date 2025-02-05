namespace RockPaperScissors.Client.Models;
public class SetUserIdCommandResult {
    public bool IsSuccess { get; set; }
    public int UserId { get; set; }
    public string? ErrorMessage { get; set; }
}
