using GetExchange_Rate.Bussines.Abstractions;

namespace GetExchange_Rate.Bussines;



public class Console : IConsole {


    private readonly ICurrencyService currencyService;

    public Console (ICurrencyService currencyService) {
        this.currencyService = currencyService;
    }


    public async Task RunAsync () {
        await currencyService.InitCache ();
        var rateList = currencyService.GetAllRates();
        System.Console.WriteLine("Консоль получения данных устоновленных курсов валют");
        System.Console.WriteLine("Список доступных валют: ");
        System.Console.WriteLine(string.Join(";\r\n", rateList.Select(r => r.Code + " " + r.CurrencyName)));
        
        int outChoice = 0;
        do {
            System.Console.Write("Введите код валюты или \"out\" для выхода  ");
            var command = System.Console.ReadLine();
            if (string.IsNullOrWhiteSpace(command) || command.Equals("out")) {
                outChoice = 0;
            } else {
                outChoice = 1;
                var currencyRate = currencyService.GetRate(command);
                if(currencyRate is null) {
                    System.Console.WriteLine("Для заданного кода курс не найден");
                } else {
                    System.Console.WriteLine($"Курс 1 {currencyRate.CurrencyName } Равен {currencyRate.Rate} RUB");
                }
            }
        } while (outChoice != 0);
    }
}
