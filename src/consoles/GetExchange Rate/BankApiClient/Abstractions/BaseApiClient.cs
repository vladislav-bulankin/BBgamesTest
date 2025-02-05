using System.Text;
using System.Xml.Serialization;


namespace GetExchange_Rate.BankApiClient.Abstractions;


public abstract class BaseApiClient {

    public async Task<TResponse?> GetAsXmlAsync<TResponse> (string uri) {
        using HttpClient client = new();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        HttpResponseMessage response = await client.GetAsync(uri);
        if (response.IsSuccessStatusCode) {
            var byteArr = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            Encoding encoding = Encoding
                .GetEncoding(
                    "Windows-1251", new EncoderReplacementFallback("?"),
                    new DecoderReplacementFallback("?")
                );
            var responseString = encoding.GetString(byteArr);
            var result = FromXmlString<TResponse>(responseString);
            return result ?? default;
        } else { throw new Exception(response.StatusCode.ToString()); }

    }

    private TValue? FromXmlString<TValue> (string? xml) { 
        XmlSerializer serializer = new XmlSerializer(typeof(TValue));
        TextReader reader = new StringReader(xml ?? string.Empty);
        var result = serializer.Deserialize(reader);
        return (TValue)result ?? default;
    }
}
