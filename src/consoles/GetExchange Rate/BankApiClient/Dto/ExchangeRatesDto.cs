using System.Xml.Serialization;



namespace GetExchange_Rate.BankApiClient.Dto;

[XmlRoot(ElementName = "ValCurs")]
public class ExchangeRatesDto {
    [XmlElement(ElementName = "Valute")]
    public List<CurrencyDto?>? Valute { get; set; }

    [XmlAttribute(AttributeName = "Date")]
    public string? Date { get; set; }//сереализатор Xml отказался парсить дату

    [XmlAttribute(AttributeName = "name")]
    public string? Name { get; set; }
}
