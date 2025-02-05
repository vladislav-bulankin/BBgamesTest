namespace RockPaperScissors.Client.Models;
public class MatchShortView {
    public int GameId { get; set; }
    public double Rate { get; set; }
    public string? UserName { get; set; }

    public override string ToString () {
        var gamer = string.IsNullOrWhiteSpace(this.UserName) ? "нет  ожидающих играков" : $"ожидает {this.UserName}";
        return $"матч id = {this.GameId} ставка {this.Rate.ToString()} {gamer}";
    }
}
