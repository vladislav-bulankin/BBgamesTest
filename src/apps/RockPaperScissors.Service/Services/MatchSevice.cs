using Grpc.Core;
using RockPaperScissors.Bussines.Abstractions;
using RockPaperScissors.Service.Protos;

namespace RockPaperScissors.Service.Services;
public class MatchSevice : Match.MatchBase {

    private readonly IMatchService matchService;
    private readonly IUserService userService;

    public MatchSevice (IMatchService matchService, IUserService userService) {
        this.matchService = matchService;
        this.userService = userService;
    }

    public override async Task GetMatchList (
        GetMatchListRequst requst,
        IServerStreamWriter<MatchGame> responseStream,
        ServerCallContext context) {
        var games = await matchService.GetAllMatchesAsync().ConfigureAwait(false);

        foreach (var game in games) {
            await responseStream.WriteAsync(new MatchGame {
                GameId = game.MatchId,
                GameRate = (double)game.Rate,
                PlayerName = game?.Player1?.UserName ?? string.Empty,
            }).ConfigureAwait(false);
        }
    }

    public override async Task<MatchGame> 
        CreateMatch (CreateMatchRequest request, ServerCallContext context) {
        var gamer = await userService.GetUserAsync(request.UserName);
        var match = await matchService
            .CreateMatch(gamer, (decimal)request.MatchRate, request.PlayerChoice);
        var result = new MatchGame { 
            GameId = match.MatchId,
            GameRate = (double)match.Rate,
            PlayerName = match?.Player1?.UserName ?? string.Empty,
        };
        return result;
    }
}
