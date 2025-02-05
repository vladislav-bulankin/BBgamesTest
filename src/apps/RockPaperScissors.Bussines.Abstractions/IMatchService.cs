using RockPaperScissors.Domain.BussnesModels;

namespace RockPaperScissors.Bussines.Abstractions;
public interface IMatchService {
    Task<IEnumerable<Match>> GetAllMatchesAsync ();

    Task<Match> CreateMatch (Gamer gamer, decimal bit, string move);
    Task<JoinToMatchResponseBM> JoinToMatchAsync (int gameId, int userId, string move);
}
