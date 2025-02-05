using RockPaperScissors.Domain.DbModels;

namespace RockPaperScissors.Bussines.Match;
internal static class Mapper {
    public static IEnumerable<Domain.BussnesModels.Match> MapMachListFromDao (
        IEnumerable<MatchHistory> matchHistories,
        IEnumerable<Users> users) {

        var result = matchHistories.Select(m => new Domain.BussnesModels.Match {
            MatchId = m.Id,
            Rate = m.Bet,
            Player1 = MapGamerFromUserDao(
                users.FirstOrDefault(u => u.Id == m.Player1Id)),
            Player2 = MapGamerFromUserDao(
                users.FirstOrDefault(u => u.Id == m.Player2Id)),
        });
        return result;
    }

    public static Domain.BussnesModels.Gamer
        MapGamerFromUserDao(Users gamer) {
        if (gamer is null) { return null; }
        return new Domain.BussnesModels.Gamer {
            Id = gamer.Id,
            UserName = gamer.UserName
        };
    }

    public static MatchHistory CreateMath 
        (Domain.BussnesModels.Gamer gamer, decimal bit, string move) {
        return new() {
            Player1Id = gamer.Id,
            Player2Id = null,
            WinnerId = null,
            Bet = bit,
            MovePlayer1 = move,
            MovePlayer2 = string.Empty,
            CreatedAt = DateTime.Now.ToUniversalTime(),
        };
    }

    public static void 
        MapUpdateMatch (ref MatchHistory entity, int userId, string userChoice) {

        int? winId = SetWinner(
            (entity.MovePlayer1, userChoice),
            (entity?.Player1Id ?? 0, userId));

        entity.Player2Id = userId ;
        entity.MovePlayer2 = userChoice;
        entity.WinnerId = winId;
    }

    private static int? SetWinner (
        (string? mPl1, string? mPl2) playersChoices, 
        (int pl1, int pl2) playersIds){
        return playersChoices switch {
            ("R", "P") => playersIds.pl2,
            ("P", "R") => playersIds.pl1,
            ("R", "S") => playersIds.pl1,
            ("S", "R") => playersIds.pl2,
            ("S", "P") => playersIds.pl1,
            ("P", "S") => playersIds.pl2,
            _ => null
        };
    }
}
