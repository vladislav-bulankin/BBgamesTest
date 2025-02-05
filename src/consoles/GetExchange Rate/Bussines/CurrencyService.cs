using GetExchange_Rate.BankApiClient.Abstractions;
using GetExchange_Rate.Bussines.Abstractions;
using GetExchange_Rate.Domain.Models;
using System.Globalization;

namespace GetExchange_Rate.Bussines;
public class CurrencyService : ICurrencyService {

    private readonly ICentralBankApiClient centralBankApiClient;

    private readonly HashSet<CurencyRate> ratesCach;


    public CurrencyService (ICentralBankApiClient centralBankApiClient) {
        this.centralBankApiClient = centralBankApiClient;
        ratesCach = new ();
    }


    public async Task InitCache () {
        try {
            var currencyRates = await centralBankApiClient.GetExchangeRatesAsync();

            currencyRates?.Valute?.Select(cur => new CurencyRate() {
                Code = cur?.ID,
                Rate = double.Parse(cur?.VunitRate ?? "0,0", new CultureInfo("ru-ru")),
                CurrencyName = cur?.Name
            }).ForEach(rate => {
                ratesCach.Add(rate);
            });
        } catch (Exception ex) {
            throw new Exception("не удолось загрузить курсы валют от банка");
        }
        
    }

    public IEnumerable<CurencyRate?>? GetAllRates () {
        return ratesCach.ToList();
    }

    public CurencyRate? GetRate (string currencyCode) {
       return ratesCach.FirstOrDefault(x => x.Code == currencyCode);
    }

}
