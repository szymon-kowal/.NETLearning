using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Services.FinnhubService {

	public class FinnHubService : IFinnHubService {
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public FinnHubService(IConfiguration configuration, IHttpClientFactory httpClientFactory) {
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public Dictionary<string, object>? GetCompanyProfile(string stockSymbol) {
			return GetDataFromFinnhub("stock/profile2", stockSymbol);
		}

		public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol) {
			return GetDataFromFinnhub("quote", stockSymbol);
		}

		private Dictionary<string, object>? GetDataFromFinnhub(string placeToCall, string stockSymbol) {
			string url = $"https://finnhub.io/api/v1/{placeToCall}?symbol={stockSymbol}&token={_configuration["FinnhubAPIKey"]}";

			using HttpClient client = _httpClientFactory.CreateClient();

			HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage responseMessage = client.Send(requestMessage);

			Stream stream = responseMessage.Content.ReadAsStream();
			StreamReader reader = new StreamReader(stream);

			string value = reader.ReadToEnd();

			Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(value);

			return result;
		}
	}
}