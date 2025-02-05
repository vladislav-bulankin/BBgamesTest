using System.ComponentModel;

namespace GetExchange_Rate.Domain.Models;

public class CurencyRate {

    [Description("Код валюты")]
    public string? Code { get; set; }
    [Description("Курс валюты")]
    public double? Rate { get; set; }
    [Description("Наименование")]
    public string? CurrencyName { get; set; }
}
