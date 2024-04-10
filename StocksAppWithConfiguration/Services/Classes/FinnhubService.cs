using StocksAppWithConfiguration.Services.Interfaces;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StocksAppWithConfiguration.Services.Classes {

    public class FinnhubService : IFinnhubService {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory) {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public Task<Dictionary<string, object>> GetCompanyProfile(string stockSymbol) {
            return GetDataFromFinnhub("stock/profile2", stockSymbol);
        }

        public Task<Dictionary<string, object>> GetStockPriceQuote(string stockSymbol) {
            return GetDataFromFinnhub("quote", stockSymbol);
        }

        private async Task<Dictionary<string, object>> GetDataFromFinnhub(string placeToCall, string stockSymbol) {
            string url = $"https://finnhub.io/api/v1/{placeToCall}?symbol={stockSymbol}&token={_configuration["FinnhubAPIKey"]}";
            using HttpClient client = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(stream);

            string value = streamReader.ReadToEnd();
            Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(value);

            if (result is null) {
                throw new InvalidOperationException("No response from finnhub server");
            }

            if (result.ContainsKey("error")) {
                throw new InvalidOperationException(Convert.ToString(result["error"]));
            }
            return result;
        }
    }
}