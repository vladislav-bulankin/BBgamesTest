namespace RockPaperScissors.Domain.BussnesModels;
public class Match {
    public int MatchId { get; set; }
    public decimal Rate { get; set; }
    public Gamer? Player1 { get; set; }
    public Gamer? Player2 { get; set; }
}

