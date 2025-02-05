using GetExchange_Rate.BankApiClient.Dto;

namespace GetExchange_Rate.BankApiClient.Abstractions;
public interface ICentralBankApiClient {

    Task<ExchangeRatesDto?> GetExchangeRatesAsync ();

}
