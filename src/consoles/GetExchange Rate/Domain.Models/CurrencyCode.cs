using System.ComponentModel;

namespace GetExchange_Rate.Domain.Models;


public class CurrencyCode {

    [Description("Код валюты")]
    public string? CurrencyId { get; set; }

    [Description("Наименование валюты")]
    public string? CurrencyName { get; set; }

}
