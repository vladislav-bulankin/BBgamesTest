using Grpc.Core;
using RockPaperScissors.Bussines.Abstractions;
using RockPaperScissors.Service.Protos;

namespace RockPaperScissors.Service.Services;
public class UserService : User.UserBase {

    private readonly IUserService userService;
    private readonly IMatchService matchService;

    public UserService (IUserService userService, IMatchService matchService) {
        this.userService = userService;
        this.matchService = matchService;
    }

    public override async Task<IdentityResponse> IdentifyUser
        (IdentityRequest request, ServerCallContext context) {
        try {
            var isExists = await userService.CheckExistsAsync(request.UserName);
            if (isExists) {
                return await GetExistsUserAsync(request.UserName);
            } else {
                return await GetNewUserAsync(request.UserName);
            }
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public override async Task<UserBalance> GetBalance(Gamer gamer, ServerCallContext context) {
        var balance = await userService.GetUserBalanceAsync(gamer.UserId);
        return new() {
            Balance = balance,
        };
    }

    public override async Task<JoinResponse> JoinToMatch
        (JoinToMatchRequst request, ServerCallContext context) {
        var user = await userService.GetUserAsync(request.UserName);
        if(user is null) { throw new ArgumentNullException(nameof(user)); }
        var response = await matchService
            .JoinToMatchAsync(request.GameId, user.Id, request.Move);
        if (response is not null && response.IsSuccess) {
            //игра завершилась надо отправить message первому игроку
            var notificationService = new NotificationService();
            await notificationService.SendNotification(response);
        }
        
        return new() {
            GameId = response?.GameId ?? request.GameId,
            Player1Name = response?.Player1Name,
            Player1Move = response?.Player1Move,
            Player2Name = response?.Player2Name,
            Player2Move = response?.Player2Move,
            IsSuccess = response?.IsSuccess ?? false
        };
    }

    public override async Task<MoneytransferResponse> CreateMoneyTransfer
        (MoneyTransferRequest request, ServerCallContext context) {
        var response = await userService
            .CreateMoneyTransferAsync(request.ToUserId, request.FromUserId, request.Amount);
        return new() {
            IsSuccess = response.IsSuccess,
            Error = response.Error,
        };
    }

    private async Task<IdentityResponse> GetExistsUserAsync(string userName) {
        var user = await userService.GetUserAsync(userName);
        if (user is null) { throw new NullReferenceException("user is null"); }
        return new() {
            IsSuccess = true,
            UserId = user.Id,
        };
    }

    private async Task<IdentityResponse> GetNewUserAsync(string userName) {
        var user = await userService.CreateUserAsync(userName);
        if (user is null) { throw new NullReferenceException("user is null"); }
        return new() {
            IsSuccess = true,
            UserId = user.Id,
        };
    }
}
