using System.Xml.Serialization;



namespace GetExchange_Rate.BankApiClient.Dto;



[XmlRoot(ElementName = "Valute")]
public class CurrencyDto {
    [XmlElement(ElementName = "NumCode")]
    public int NumCode { get; set; }

    [XmlElement(ElementName = "CharCode")]
    public string? CharCode { get; set; }

    [XmlElement(ElementName = "Nominal")]
    public int Nominal { get; set; }

    [XmlElement(ElementName = "Name")]
    public string? Name { get; set; }

    [XmlElement(ElementName = "Value")]
    public string? Value { get; set; }//сереализатор Xml отказался парсить в double 

    [XmlElement(ElementName = "VunitRate")]
    public string? VunitRate { get; set; }//сереализатор Xml отказался парсить в double 

    [XmlAttribute(AttributeName = "ID")]
    public string? ID { get; set; }

}
/*
 [XmlElement(ElementName="NumCode")] 
	public int NumCode { get; set; } 

	[XmlElement(ElementName="CharCode")] 
	public string CharCode { get; set; } 

	[XmlElement(ElementName="Nominal")] 
	public int Nominal { get; set; } 

	[XmlElement(ElementName="Name")] 
	public string Name { get; set; } 

	[XmlElement(ElementName="Value")] 
	public double Value { get; set; } 

	[XmlElement(ElementName="VunitRate")] 
	public double VunitRate { get; set; } 

	[XmlAttribute(AttributeName="ID")] 
	public string ID { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
 */