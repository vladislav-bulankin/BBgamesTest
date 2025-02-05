using GetExchange_Rate.Domain.Models;

namespace GetExchange_Rate.Bussines.Abstractions;


public interface ICurrencyService {

    Task InitCache ();
    IEnumerable<CurencyRate?>? GetAllRates ();
    CurencyRate? GetRate (string currencyCode);

}
