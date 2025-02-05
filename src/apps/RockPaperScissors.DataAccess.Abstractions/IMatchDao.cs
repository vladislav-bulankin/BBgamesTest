using RockPaperScissors.Domain.DbModels;

namespace RockPaperScissors.DataAccess.Abstractions;
public interface IMatchDao {
    Task<int> CreateMatchAsync (MatchHistory matchHistoryDao);
    Task<IEnumerable<MatchHistory>> GetAllMatchesAsync ();
    Task<MatchHistory?> GetMatchAsync (int matchId);
    Task UpdateMatchAsync (MatchHistory newEntiy);
}
