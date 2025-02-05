namespace RockPaperScissors.Domain.BussnesModels;


public enum OperationsType {
    FromUser = 1,//входящий перевод от другова пользователя
    ToUser = 2,//исходящий перевод другому пользователю
    AddingBalans = 3,//пополнение баланса
    WithdrawalFunds = 4,//вывод средств
    None = 0
}
