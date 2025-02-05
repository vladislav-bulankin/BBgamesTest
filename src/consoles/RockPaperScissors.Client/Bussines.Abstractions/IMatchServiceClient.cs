using RockPaperScissors.Client.Models;

namespace RockPaperScissors.Client.Bussines.Abstractions;
public interface IMatchServiceClient {

    IAsyncEnumerable<MatchShortView> GatMatchListAsync ();

    Task<MatchDto> CreateMatchAsync (double bet, string userMove);
}
