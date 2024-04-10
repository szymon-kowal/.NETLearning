using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppWithConfiguration.Models;
using StocksAppWithConfiguration.Services.Interfaces;
using System.Text.Json;

namespace StocksAppWithConfiguration.Controllers {

	public class TradeController : Controller {
		private readonly IFinnhubService _finnhubService;
		private readonly IOptions<TradingOptions> _options;
		private readonly IConfiguration _configuration;

		public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> options, IConfiguration configuration) {
			_finnhubService = finnhubService;
			_options = options;
			_configuration = configuration;
		}

		[Route("/")]
		public async Task<IActionResult> Index() {
			string? defaultSymbol = _options.Value.DefaultStockSymbol;
			if (defaultSymbol == null) {
				defaultSymbol = "MSFT";
			}
			Dictionary<string, object> stockPriceQuote = await _finnhubService.GetStockPriceQuote(defaultSymbol);
			Dictionary<string, object> companyProfileService = await _finnhubService.GetCompanyProfile(defaultSymbol);
			StockTrade stockTrade = new StockTrade() {
				StockName = companyProfileService["name"].ToString(),
				StockSymbol = defaultSymbol,
				Price = Convert.ToDouble(((JsonElement) stockPriceQuote["c"]).GetDouble()),
				Quantity = Convert.ToDouble(((JsonElement) companyProfileService["shareOutstanding"]).GetDouble()),
			};
			ViewBag.Key = _configuration["FinnhubAPIKey"];
			return View(stockTrade);
		}
	}
}