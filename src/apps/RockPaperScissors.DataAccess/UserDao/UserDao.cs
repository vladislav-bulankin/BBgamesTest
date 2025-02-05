using Microsoft.EntityFrameworkCore;
using RockPaperScissors.DataAccess.Abstractions;
using RockPaperScissors.Domain.DbModels;
using System.Data;

namespace RockPaperScissors.DataAccess.UserDao;

public class UserDao : IUserDao {

    public async Task<int> AddUserAsync (Users user) {
        using AppDbContext db = new();
        var entity = await db.UserDb.AddAsync(user);
        await db.SaveChangesAsync();
        return entity.Entity.Id;
    }

    public async Task<double> GetIncomingPayis(int userId) {
        using AppDbContext db = new();
        var incoming = await db
            .GameTransactionsDb
            .Where(u => u.UserId == userId && (u.OperationType == 1 || u.OperationType == 3))
            .SumAsync(a => a.Amount);
        return (double)incoming;
    }

    public async Task<double> GetOutgoingPayis(int userId) {
        using AppDbContext db = new();
        var outgoing = await db
           .GameTransactionsDb
           .Where(u => u.UserId == userId && (u.OperationType == 2 || u.OperationType == 4))
           .SumAsync(a => a.Amount);
        return (double)(outgoing);
    }

    public async Task<bool> CheckExistsAsync (string userName) {
        using AppDbContext db = new();
        return await db.UserDb.AnyAsync(u => u.UserName == userName);
    }

    public async Task<Users?> GetUserAsync (string userName) {
        using AppDbContext db = new();
        return await db.UserDb.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<Users?> GetUserAsync (int userId) {
        using AppDbContext db = new();
        return await db.UserDb.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<Users>> GetUsersAsync (params int?[] ids) {
        using AppDbContext db = new();
        var result = await db.UserDb.Where(u => ids.Contains(u.Id)).ToListAsync();
        return result;
    }

    public async Task<Users?> GetUserById(int userId) {
        using AppDbContext db = new();
        return await db.UserDb.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task CreateUserTransferAsync (GameTransactions transaction) {
        using AppDbContext db = new();
        await db.GameTransactionsDb.AddAsync(transaction);
        await db.SaveChangesAsync();
    }

}
