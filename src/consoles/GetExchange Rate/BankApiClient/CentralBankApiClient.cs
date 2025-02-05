using GetExchange_Rate.BankApiClient.Abstractions;
using GetExchange_Rate.BankApiClient.Dto;
using Microsoft.Extensions.Configuration;

namespace GetExchange_Rate.BankApiClient;


public class CentralBankApiClient : BaseApiClient, ICentralBankApiClient {

    private readonly string rateUri;
    
    public CentralBankApiClient (IConfiguration configuration) {
        rateUri = configuration
            ?.GetSection("CentroBankEndPoints")
            ?.GetSection("rateUri")
            ?.Value  ?? string.Empty;
    }

    public Task<ExchangeRatesDto?> GetExchangeRatesAsync () {
        return GetAsXmlAsync<ExchangeRatesDto>(rateUri);
    }

}
