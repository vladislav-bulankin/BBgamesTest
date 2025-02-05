using RockPaperScissors.Bussines.Abstractions;
using RockPaperScissors.DataAccess.Abstractions;
using RockPaperScissors.Domain.BussnesModels;

namespace RockPaperScissors.Bussines.Match;
public class MatchService : IMatchService {

    private readonly IMatchDao matchDao;
    private readonly IUserDao userDao;
    private IUserService userService;

    public MatchService (
        IMatchDao matchDao, 
        IUserDao userDao,
        IUserService userService) {
        this.matchDao = matchDao;
        this.userDao = userDao;
        this.userService = userService;
    }

    public async Task<IEnumerable<Domain.BussnesModels.Match>> GetAllMatchesAsync () {
        var gamesDaoList = await matchDao.GetAllMatchesAsync();

        var gamersIds = gamesDaoList
                .Where(p => p.Player1Id > 0)
                .Select(g => g.Player1Id).ToArray();

        var gamers = await userDao.GetUsersAsync(gamersIds);
        var result = Mapper.MapMachListFromDao(gamesDaoList, gamers);
        return result;
    }

    public async Task<Domain.BussnesModels.Match> 
        CreateMatch (Gamer gamer, decimal bit, string move) {

        var dao = Mapper.CreateMath(gamer, bit, move);
        var machId = await matchDao.CreateMatchAsync(dao);
        var result = new Domain.BussnesModels.Match {
            MatchId = machId,
            Rate = bit,
            Player1 = gamer,
            Player2 = null
        };
        return result;
    }

    public async Task<JoinToMatchResponseBM> 
        JoinToMatchAsync (int gameId, int userId, string move) {
        //palyer1 это тот что ужее в игре palyer2 тот который присоеденился
        var game = await  matchDao.GetMatchAsync(gameId);
        var palyer1 = await userDao.GetUserById(game.Player1Id ?? 0);
        var palyer2 = await userDao.GetUserAsync(userId);
        bool isSuccess = true;
        if (game is not null && palyer2 is not null) {
            lock (this) {
                if (game.Player1Id != userId//если игрок в игре не текущий
                    && (game.Player2Id ?? 0) == 0) {//и второго игрока нет
                    try {
                        //победитель определится в маппере
                        Mapper.MapUpdateMatch(ref game, userId, move);
                        //после обновления игра завершена
                        matchDao.UpdateMatchAsync(game).Wait();
                    } catch (Exception) { isSuccess = false; }
                }
            }
            if (isSuccess && (game?.WinnerId ?? 0) > 0) {
                var loserId = game.WinnerId == game.Player1Id ? game.Player2Id : game.Player1Id;
                await userService
                    .CreateMoneyTransferAsync(
                        game?.WinnerId ?? 0, 
                        loserId ?? 0, 
                        (double)game.Bet
                    );
            }

            var winName = game.WinnerId is null ? "ничья" : 
                game.WinnerId == palyer1.Id ?
                    palyer1?.UserName : palyer2?.UserName;

            return new() {
                GameId = game.Id,
                Player1Name = palyer1?.UserName ?? string.Empty,
                Player2Name = palyer2?.UserName ?? string.Empty,
                Player1Move = game.MovePlayer1,
                Player2Move = game.MovePlayer2,
                WinnerId = game.WinnerId ?? 0,
                WinnerName = winName,
                Player1Id = palyer1.Id,
                IsSuccess = isSuccess,
            };
        }
        var name = game is null ? nameof(gameId) : nameof(palyer2);
        throw new Exception($"not found {name}");
    }
}
