using Microsoft.EntityFrameworkCore;
using RockPaperScissors.DataAccess.Abstractions;
using RockPaperScissors.Domain.DbModels;

namespace RockPaperScissors.DataAccess.MatchDao;
public class MatchDao : IMatchDao {
    public async Task<int> CreateMatchAsync (MatchHistory matchHistoryDao) {
        using AppDbContext db = new();
        var entity = await db.MatchHistoryDb.AddAsync(matchHistoryDao);
        await db.SaveChangesAsync();
        return entity.Entity.Id;
    }

    public async Task<IEnumerable<MatchHistory>> GetAllMatchesAsync () {
        using AppDbContext db = new();
        return await db.MatchHistoryDb.Where(w => w.WinnerId == null && w.Player2Id == null).ToListAsync();
    }

    public async Task<MatchHistory?> GetMatchAsync (int matchId) {
        using AppDbContext db = new();
        return await db.MatchHistoryDb.FirstOrDefaultAsync(m => m.Id == matchId);
    }

    public async Task UpdateMatchAsync (MatchHistory newEntiy) {
        using AppDbContext db = new();
        db.MatchHistoryDb.Update(newEntiy);
        await db.SaveChangesAsync();
    }
}
