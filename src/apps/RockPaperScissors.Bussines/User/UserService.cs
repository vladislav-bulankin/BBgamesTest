using RockPaperScissors.Bussines.Abstractions;
using RockPaperScissors.DataAccess.Abstractions;
using RockPaperScissors.Domain.BussnesModels;
using System;

namespace RockPaperScissors.Bussines.User;
public class UserService : IUserService {

    private readonly IUserDao userDaoService;


    public UserService (IUserDao userDaoService) {
        this.userDaoService = userDaoService;
    }
    public Task<bool> CheckExistsAsync (string userName) {
        return userDaoService.CheckExistsAsync(userName);
    }

    public async Task<UserMoneyTransferResponse> 
        CreateMoneyTransferAsync (int to, int from, double amount) {
        if (from == to) {
            var addingAccountOperation = Mapper
                .CreateTransfer(to, amount, OperationsType.AddingBalans);
            await userDaoService.CreateUserTransferAsync(addingAccountOperation);
            return CreateSuccessResponse();
        }
        var incomingPayment = Mapper.CreateTransfer(to, amount, OperationsType.FromUser);
        var outgoingPayment = Mapper.CreateTransfer(from, amount, OperationsType.ToUser);
        var paymet1 = userDaoService.CreateUserTransferAsync(incomingPayment);
        var paymet2 = userDaoService.CreateUserTransferAsync(outgoingPayment);
        await Task.WhenAll(paymet1, paymet2);
        return CreateSuccessResponse();
    }

    public async Task<Gamer?> CreateUserAsync (string userName) {
        int userId = await userDaoService.AddUserAsync(new() { UserName = userName });
        var userDao = await userDaoService.GetUserAsync(userId);
        if (userDao is null) { throw new Exception("user not create"); }
        return new() {
            Id = userDao.Id,
            UserName = userDao.UserName,
        };
    }

    public async Task<Gamer> GetUserAsync (string userName) {
        var bussines = await userDaoService.GetUserAsync(userName);
        if (bussines is null) { throw new Exception("user not found"); }
        return new() {
            Id = bussines.Id,
            UserName = bussines.UserName
        };
    }

    public async Task<double> GetUserBalanceAsync (int userId) {
        var incoming = await userDaoService.GetIncomingPayis(userId);
        var outgoing = await userDaoService.GetOutgoingPayis(userId);
        var balance = incoming - outgoing;
        return balance;
    }

    private UserMoneyTransferResponse CreateSuccessResponse () {
        return new() {
            IsSuccess = true,
            Error = string.Empty
        };
    }
}
